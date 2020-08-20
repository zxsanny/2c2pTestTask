using System;

namespace TransactionManager.Parsers
{
    public class FileParserFactory : ITransactionFileParserFactory
    {
        public ITxFileParser CreateTransactionFileParser(string extension)
        {
            switch (extension)
            {
                case "xml": return new TransactionXmlParser();
                case "csv": return new TransactionCsvParser();
                default:
                    throw new Exception("Unknown format");
            }
        }
    }
}
