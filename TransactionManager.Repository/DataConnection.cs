using LinqToDB.Configuration;
using LinqToDB.Data;
using System.Linq;
using TransactionManager.Common.Entities;

namespace TransactionManager.Repository
{
    public class TransactionsDataConnection : DataConnection
    {
        public TransactionsDataConnection(LinqToDbConnectionOptions<TransactionsDataConnection> options) : base(options)
        {
        }

        public IQueryable<T> Get<T>() where T : class 
            => GetTable<T>();
    }
}
