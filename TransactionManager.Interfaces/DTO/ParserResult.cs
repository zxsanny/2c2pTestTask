using System.Collections.Generic;

namespace TransactionManager.Parsers
{
    public class ParserResult
    {
        public bool Success { get; set; }
        public List<TransactionInfo> Transactions { get; set; }
        public List<string> Errors { get; set; }
    }
}