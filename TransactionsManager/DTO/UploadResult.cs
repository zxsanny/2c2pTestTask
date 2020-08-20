using TransactionManager.Parsers;
using TransactionManager.Repository;

namespace TransactionsManager.DTO
{
    public class UploadResult
    {
        public ParserResult ParserResult { get; set; }
        public InsertResult InsertResult { get; set; }

        public UploadResult(ParserResult parserResult, InsertResult insertResult = null)
        {
            ParserResult = parserResult;
            InsertResult = insertResult;
        }
    }
}
