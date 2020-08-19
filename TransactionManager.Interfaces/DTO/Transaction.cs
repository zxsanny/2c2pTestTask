using System;

namespace TransactionManager.Importers
{
    public class Transaction
    {
        public string Id { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public DateTime Date { get; set; }
        public StatusEnum Status { get; set; }
    }

    public enum StatusEnum
    {
        A,
        R,
        D
    }
}