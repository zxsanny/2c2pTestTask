namespace TransactionManager.Parsers
{
    public interface ITransactionFileParserFactory
    {
        ITransactionFileParser CreateTransactionFileParser(string fileName);
    }
}