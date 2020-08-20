using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using TinyCsvParser;
using TinyCsvParser.Mapping;
using TinyCsvParser.TypeConverter;
using TransactionManager.Common.Entities;

namespace TransactionManager.Parsers
{
    public class TxCsvParser : BaseTXFileParser
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
                MapProperty(3, x => x.Date, new DateTimeConverter("dd/MM/yyyy HH:mm:ss"));
                MapProperty(4, x => x.Status, new EnumConverter<CSVTransactionStatusEnum>());
            }
        }
        public override ParserResult ParseStream(Stream filestream)
        {
            try
            {
                var parser = new CsvParser<CSVTransaction>(new CsvParserOptions(false, FIELDS_SEPARATOR), new CsvTransactionMapping());
                var txs = parser.ReadFromStream(filestream, Encoding.Default).ToList();
                return Process(txs, tx => tx.IsValid,
                    tx => new TransactionInfo(tx.Result.Id, tx.Result.Amount, tx.Result.Currency, tx.Result.Date, EnumMap[tx.Result.Status]),
                    tx => $"Row: {tx.RowIndex} Transaction Id: {tx.Result?.Id} Error: {tx.Error.Value}");
            }
            catch (Exception ex)
            {
                return new ParserResult(ex.Message);
            }
        }
    }
}
