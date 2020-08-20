using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionManager.Common.DTO;
using TransactionManager.Common.Entities;

namespace TransactionManager.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        readonly TransactionsDataConnection _connection;
        const int INSERT_BATCH_SIZE = 100;

        public TransactionRepository(TransactionsDataConnection connection)
        {
            _connection = connection;
        }

        public async Task<InsertResult> InsertAsync(IEnumerable<TransactionInfo> transactions)
        {
            await _connection.BeginTransactionAsync();
            var result = _connection.BulkCopy(new BulkCopyOptions 
            { 
                MaxBatchSize = INSERT_BATCH_SIZE,
                UseInternalTransaction = true,
                //TODO: update rows which weren't inserted due to id duplicates
                //TODO: check work of this:
                //KeepIdentity = true 
            }, transactions);
            await _connection.CommitTransactionAsync();
            return new InsertResult((int)result.RowsCopied, 0);
        }

        public async Task<IEnumerable<TransactionInfo>> GetAsync(TransactionFilter filter)
        {
            var q = _connection.Get<TransactionInfo>();
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
