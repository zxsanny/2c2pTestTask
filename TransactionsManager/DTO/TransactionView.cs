using System.Collections.Generic;
using System.Globalization;
using TransactionManager.Common.Entities;

namespace TransactionsManager.DTO
{
    public class TransactionView
    {
        public string Id { get; set; }
        public string Payment { get; set; }
        public string Status { get; set; }

        public TransactionView(TransactionInfo transaction)
        {
            Id = transaction.Id;
            Payment = $"{transaction.Amount.ToString("0.00", CultureInfo.InvariantCulture)} {transaction.Currency}";
            Status = transaction.Status.ToString().Substring(0, 1);
        }
    }
}
