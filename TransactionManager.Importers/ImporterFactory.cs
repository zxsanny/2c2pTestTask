using System;
using System.IO;

namespace TransactionManager.Importers
{
    public class ImporterFactory
    {
        IImporter CreateImporter(string fileName)
        {
            var extension = new FileInfo(fileName).Extension.ToLower();
            switch (extension)
            {
                case "xml": return new XMLImporter();
                case "csv": return new CSVImporter();
                default:
                    throw new Exception("Unknown format");
            }
        }
            
    }
}
