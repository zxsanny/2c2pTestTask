using System.Collections.Generic;
using TransactionManager.Common.Entities;

namespace TransactionManager.Parsers
{
    public class ParserResult
    {
        public bool Success { get; set; }
        public List<TransactionInfo> Transactions { get; set; }
        public List<string> Errors { get; set; }
    }
}