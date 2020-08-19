using TransactionManager.Parsers;
using TransactionManager.Repository;

namespace TransactionsManager.DTO
{
    public class UploadResult
    {
        ParserResult ParserResult { get; set; }
        InsertResult InsertResult { get; set; }

        public UploadResult(ParserResult parserResult, InsertResult insertResult = null)
        {
            ParserResult = parserResult;
            InsertResult = insertResult;
        }
    }
}
