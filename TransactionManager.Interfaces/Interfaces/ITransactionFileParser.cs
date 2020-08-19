using System.IO;

namespace TransactionManager.Parsers
{
    public interface ITransactionFileParser
    {
        ParserResult ParseFile(Stream filestream);
    }
}