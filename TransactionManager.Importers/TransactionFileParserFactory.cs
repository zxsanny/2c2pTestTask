using System;
using System.IO;

namespace TransactionManager.Parsers
{
    public class TransactionFileParserFactory : ITransactionFileParserFactory
    {
        public ITransactionFileParser CreateTransactionFileParser(string fileName)
        {
            var extension = new FileInfo(fileName).Extension.ToLower();
            switch (extension)
            {
                case "xml": return new TransactionXMLParser();
                case "csv": return new TransactionCSVParser();
                default:
                    throw new Exception("Unknown format");
            }
        }
    }
}
