using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TransactionManager.Common.DTO;
using TransactionManager.Common.Entities;

namespace TransactionManager.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        readonly TransactionsDataConnection db;
        const int INSERT_BATCH_SIZE = 100;

        public TransactionRepository(TransactionsDataConnection connection)
        {
            db = connection;
        }

        public async Task<InsertResult> InsertAsync(IEnumerable<TransactionInfo> transactions)
        {
            try
            {
                await db.BeginTransactionAsync();

                //Efficient mechanism to update A LOT of records simultaneously, utilizing additional auxiliary table.
                
                //First check whether we have duplicates by Id
                var txIds = transactions.Select(t => t.Id).ToList();
                var toUpdateIds = await db.Get<TransactionInfo>().Select(x => x.Id).Where(x => txIds.Contains(x)).ToListAsync();
                var updated = 0;

                if (toUpdateIds.Any())
                {
                    //Then if there are duplicates, bulk copy them to the _TransactionsTempUpdate table
                    db.BulkCopy(transactions.Where(t => toUpdateIds.Contains(t.Id)).Select(x => new TransactionInfoTemp(x)).ToList());
                                    //The other solution here is to utilize creation of Temp table, but it's required first of all db-specific permissions and setup, 
                                    //also not every db is supported temp table creation. That solution is more stable, but do require additional 'system' table.

                                    //Also, possibly would be worth to check in advance whether at least one field was really updated in duplicates, but it's trade off, 
                                    //cause mere checking that fact will put load on db and/or server - either big query for check, either download all data.

                    //And then run in-db query to update appropriate fields
                    updated = await db.Get<TransactionInfo>().Join(db.Get<TransactionInfoTemp>(), tx => tx.Id, temp => temp.Id, (tx, temp) => new { tx, temp })
                        .Set(v => v.tx.Amount, v => v.temp.Amount)
                        .Set(v => v.tx.Currency, v => v.temp.Currency)
                        .Set(v => v.tx.Date, v => v.temp.Date)
                        .Set(v => v.tx.Status, v => v.temp.Status)
                        .UpdateAsync();
                    
                    //Clean list to insert from already updated
                    transactions = transactions.Where(x => !toUpdateIds.Contains(x.Id)).ToList();
                    //Clean temp table
                    await db.Get<TransactionInfoTemp>().DeleteAsync();
                }

                //TODO: simultaneously inserting, utilize Task.WhenAll()
                var result = db.BulkCopy(new BulkCopyOptions
                {
                    MaxBatchSize = INSERT_BATCH_SIZE
                }, transactions);
                
                await db.CommitTransactionAsync();

                return new InsertResult(true, null, (int)result.RowsCopied, updated);
            }
            catch (Exception ex)
            {
                return new InsertResult(false, ex.Message);
            }
        }

        public async Task<IEnumerable<TransactionInfo>> GetAsync(TransactionFilter filter)
        {
            var q = db.Get<TransactionInfo>();
            if (!string.IsNullOrEmpty(filter?.Currency))
            {
                q = q.Where(x => x.Currency.ToLower().Contains(filter.Currency.ToLower()));
            }
            if (filter?.From.HasValue ?? false)
            {
                q = q.Where(x => x.Date >= filter.From);
            }
            if (filter?.To.HasValue ?? false)
            {
                q = q.Where(x => x.Date <= filter.To);
            }
            if (filter?.Status.HasValue ?? false)
            {
                q = q.Where(x => x.Status == filter.Status);
            }
            return await q.ToListAsync();
        }

    }
}
