using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionManager.Common.DTO;
using TransactionManager.Common.Entities;

namespace TransactionManager.Repository
{
    public interface ITransactionRepository
    {
        Task<InsertResult> InsertAsync(IEnumerable<TransactionInfo> transactions);
        Task<IEnumerable<TransactionInfo>> GetAsync(TransactionFilter filter);
    }
}