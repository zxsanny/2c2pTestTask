using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionManager.Parsers;

namespace TransactionManager.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        public async Task<InsertResult> InsertAsync(IEnumerable<TransactionInfo> transactions)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TransactionInfo>> GetAsync(string currency, DateTime from, DateTime to, TransactionStatusEnum status)
        {
            throw new NotImplementedException();
        }

    }
}
