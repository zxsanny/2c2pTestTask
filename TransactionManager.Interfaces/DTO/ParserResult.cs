using System.Collections.Generic;
using TransactionManager.Common.Entities;

namespace TransactionManager.Parsers
{
    public class ParserResult
    {
        public bool Success { get; set; }
        public List<TransactionInfo> Transactions { get; set; }
        public List<string> Errors { get; set; }

        public ParserResult(int possibleTxCount)
        {
            Success = true;
            Transactions = new List<TransactionInfo>(possibleTxCount);
            Errors = new List<string>();
        }

        public ParserResult(string error)
        {
            Success = false;
            Transactions = new List<TransactionInfo>();
            Errors = new List<string> { error };
        }
    }
}