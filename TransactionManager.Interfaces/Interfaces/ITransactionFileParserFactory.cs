namespace TransactionManager.Parsers
{
    public interface ITransactionFileParserFactory
    {
        ITxFileParser CreateTransactionFileParser(string fileName);
    }
}