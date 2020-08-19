using System.IO;

namespace TransactionManager.Importers
{
    public interface IImporter
    {
        ImportResult Import(FileStream file);
    }
}