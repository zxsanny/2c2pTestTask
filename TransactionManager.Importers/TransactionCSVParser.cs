using System;
using System.IO;
using System.Threading.Tasks;

namespace TransactionManager.Parsers
{
    public class TransactionCSVParser : ITransactionFileParser
    {
        public async Task<ParserResult> ParseFileAsync(Stream file)
        {
            throw new NotImplementedException();
        }
    }

    public class CSVData
    {

    }
}
