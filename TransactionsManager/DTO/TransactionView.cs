using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionManager.Common.Entities;

namespace TransactionsManager.DTO
{
    public class TransactionView
    {
        Dictionary<TransactionStatusEnum, StatusEnum> EnumMap = new Dictionary<TransactionStatusEnum, StatusEnum>
        {
            { TransactionStatusEnum.Approved, StatusEnum.A},
            { TransactionStatusEnum.Rejected, StatusEnum.R},
            { TransactionStatusEnum.Done, StatusEnum.D},
        };

        public string Id { get; set; }
        public string Payment { get; set; }
        public StatusEnum Status { get; set; }

        public TransactionView(TransactionInfo transaction)
        {
            Id = transaction.Id;
            Payment = $"{transaction.Amount} {transaction.Currency}";
            Status = EnumMap[transaction.Status];
        }
    }

    public enum StatusEnum
    {
        A,
        R,
        D
    }
}
