using System.IO;

namespace TransactionManager.Parsers
{
    public interface ITransactionFileParser
    {
        ParserResult ParseStream(Stream filestream);
    }
}