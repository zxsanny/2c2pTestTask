using System;

namespace TransactionManager.Parsers
{
    public class FileParserFactory : ITransactionFileParserFactory
    {
        public ITxFileParser CreateTransactionFileParser(string extension)
        {
            switch (extension)
            {
                case "xml": return new TxXmlParser();
                case "csv": return new TxCsvParser();
                default:
                    throw new Exception("Unknown format");
            }
        }
    }
}
