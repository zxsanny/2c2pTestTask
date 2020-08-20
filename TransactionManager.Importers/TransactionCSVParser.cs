using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TinyCsvParser;
using TinyCsvParser.Mapping;
using TinyCsvParser.TypeConverter;

namespace TransactionManager.Parsers
{
    public class TransactionCSVParser : ITransactionFileParser
    {
        const char FIELDS_SEPARATOR = ',';

        public class CSVTransaction
        {
            public string Id { get; set; }
            public decimal Amount { get; set; }
            public string Currency { get; set; }
            public DateTime Date { get; set; }
            public CSVTransactionStatusEnum Status { get; set; }
        }

        public enum CSVTransactionStatusEnum
        {
            Approved,
            Failed,
            Finished
        }

        private Dictionary<CSVTransactionStatusEnum, TransactionStatusEnum> EnumMap = new Dictionary<CSVTransactionStatusEnum, TransactionStatusEnum>
        {
            { CSVTransactionStatusEnum.Approved, TransactionStatusEnum.Approved},
            { CSVTransactionStatusEnum.Finished, TransactionStatusEnum.Done},
            { CSVTransactionStatusEnum.Failed, TransactionStatusEnum.Rejected}
        };

    public class CsvTransactionMapping : CsvMapping<CSVTransaction>
        {
            public CsvTransactionMapping() : base()
            {
                MapProperty(0, x => x.Id);
                MapProperty(1, x => x.Amount);
                MapProperty(2, x => x.Currency);
                MapProperty(3, x => x.Date);
                MapProperty(4, x => x.Status, new EnumConverter<CSVTransactionStatusEnum>());
            }
        }
        public ParserResult ParseFile(Stream filestream)
        {
            var parser = new CsvParser<CSVTransaction>(new CsvParserOptions(true, FIELDS_SEPARATOR), new CsvTransactionMapping());
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
                    //Simple constructor, don't like solutions like Automapper - it's getting cumbersome and fragile - 
                    //first, register in another place, and after some entity change, errors will be shown only in runtime
                    result.Transactions.Add(new TransactionInfo(tx.Result.Id, tx.Result.Amount, tx.Result.Currency, tx.Result.Date, EnumMap[tx.Result.Status]));
                }
                else 
                {
                    result.Success = false;
                    result.Errors.Add($"Row: {tx.RowIndex} Transaction Id: {tx.Result?.Id} Error: {tx.Error.Value}");
                }
            }
            return result;
        }
    }

    public class CSVData
    {

    }
}
