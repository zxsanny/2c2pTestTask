using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TransactionManager.Common.Entities;

namespace TransactionManager.Parsers
{
    public interface ITransactionFileParser
    {
        ParserResult ParseStream(Stream filestream);
    }

    public abstract class BaseTransactionFileParser : ITransactionFileParser
    {
        public abstract ParserResult ParseStream(Stream filestream);
        
        public ParserResult Process<T>(IEnumerable<T> list, Func<T, bool> isValid, Func<T, TransactionInfo> createTx, Func<T, string> error)
        {
            var result = new ParserResult(list.Count());
            foreach (var tx in list)
            {
                if (isValid(tx))
                {
                    result.Transactions.Add(createTx(tx));
                }
                else
                {
                    result.Success = false;
                    result.Errors.Add(error(tx));
                }
            }
            return result;
        }
    }
}