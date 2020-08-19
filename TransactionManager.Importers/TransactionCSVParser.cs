using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCsvParser;
using TinyCsvParser.Mapping;

namespace TransactionManager.Parsers
{
    public class TransactionCSVParser : ITransactionFileParser
    {
        const char FIELDS_SEPARATOR = ',';

        public class CsvTransactionMapping : CsvMapping<TransactionInfo>
        {
            public CsvTransactionMapping() : base()
            {
                MapProperty(0, x => x.Id);
                MapProperty(1, x => x.Amount);
                MapProperty(2, x => x.Currency);
                MapProperty(3, x => x.Date);
                MapProperty(4, x => x.Status);
            }
        }
        public ParserResult ParseFile(Stream filestream)
        {
            var parser = new CsvParser<TransactionInfo>(new CsvParserOptions(true, ','), new CsvTransactionMapping());
            var txs = parser.ReadFromStream(filestream, Encoding.Default).ToList();
            
            var result = new ParserResult()
            {
                Success = true,
                Transactions = new List<TransactionInfo>(txs.Count),
                Errors = new List<string>()
            };
            foreach (var tx in txs)
            {
                if (tx.IsValid)
                {
                    result.Transactions.Add(tx.Result);
                }
                else 
                {
                    result.Success = false;
                    result.Errors.Add(tx.Error.Value);
                }
            }
            return result;
        }
    }

    public class CSVData
    {

    }
}
