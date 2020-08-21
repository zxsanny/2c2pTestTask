using System;

namespace TransactionManager.Common.Entities
{
    public class TransactionInfoTemp : TransactionInfo
    {
        public TransactionInfoTemp() { }
        
        public TransactionInfoTemp(TransactionInfo transaction)
        {
            Id = transaction.Id;
            Amount = transaction.Amount;
            Currency = transaction.Currency;
            Date = transaction.Date;
            Status = transaction.Status;
        }
    }
}