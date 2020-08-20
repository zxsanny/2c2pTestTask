using System;
using TransactionManager.Common.Entities;

namespace TransactionManager.Common.DTO
{
    public class TransactionFilter
    {
        public string Currency { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public TransactionStatusEnum? Status { get; set; }

        public TransactionFilter() { }
        public TransactionFilter(string currency = null, DateTime? from = null, DateTime? to = null, TransactionStatusEnum? status = null)
        {
            Currency = currency;
            From = from;
            To = to;
            Status = status;
        }
    }
}
