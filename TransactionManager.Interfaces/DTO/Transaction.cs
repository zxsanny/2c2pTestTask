using System;

namespace TransactionManager.Parsers
{
    public class TransactionInfo
    {
        public string Id { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public DateTime Date { get; set; }
        public TransactionStatusEnum Status { get; set; }

        public TransactionInfo() { }

        public TransactionInfo(string id, decimal amount, string currency, DateTime date, TransactionStatusEnum status)
        {
            Id = id;
            Amount = amount;
            Currency = currency;
            Date = date;
            Status = status;
        }
    }

    public enum TransactionStatusEnum
    {
        Approved,
        Rejected,
        Done
    }
}