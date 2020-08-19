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
    }

    public enum TransactionStatusEnum
    {
        A,
        R,
        D
    }
}