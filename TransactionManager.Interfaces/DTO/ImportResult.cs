using System.Collections.Generic;

namespace TransactionManager.Importers
{
    public class ImportResult
    {
        public bool Success { get; set; }
        public List<Transaction> Transactions { get; set; }
        public List<string> Errors { get; set; }
    }
}