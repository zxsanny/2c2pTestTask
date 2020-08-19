using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionManager.Parsers;

namespace TransactionManager.Repository
{
    public interface ITransactionRepository
    {
        Task<InsertResult> InsertAsync(IEnumerable<TransactionInfo> transactions);
        Task<IEnumerable<TransactionInfo>> GetAsync(string currency, DateTime from, DateTime to, TransactionStatusEnum status);
    }
}