using System.IO;
using System.Threading.Tasks;

namespace TransactionManager.Parsers
{
    public interface ITransactionFileParser
    {
        Task<ParserResult> ParseFileAsync(Stream file);
    }
}