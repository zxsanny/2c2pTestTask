using System;

namespace TransactionManager.Parsers
{
    public class FileParserFactory : ITransactionFileParserFactory
    {
        public ITransactionFileParser CreateTransactionFileParser(string extension)
        {
            switch (extension)
            {
                case "xml": return new TransactionXmlParser();
                case "csv": return new TransactionCsvParser();
                default:
                    throw new ArgumentException("Unknown format");
            }
        }
    }
}
