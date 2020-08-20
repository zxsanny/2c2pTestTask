using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using TransactionManager.Common.Entities;

namespace TransactionManager.Parsers
{
    public class TransactionXmlParser : BaseTXFileParser
    {
        Dictionary<XMLTransactionStatus, TransactionStatusEnum> EnumMap = new Dictionary<XMLTransactionStatus, TransactionStatusEnum>
        {
            { XMLTransactionStatus.Approved, TransactionStatusEnum.Approved},
            { XMLTransactionStatus.Done, TransactionStatusEnum.Done},
            { XMLTransactionStatus.Rejected, TransactionStatusEnum.Rejected}
        };

        public override ParserResult ParseStream(Stream filestream)
        {
            try
            {
                var txs = new XmlSerializer(typeof(XMLTransactions)).Deserialize(filestream) as XMLTransactions;
                return Process(txs.Transactions, tx => tx.IsValid,
                    tx => new TransactionInfo(tx.Id, tx.PaymentDetails.Amount, tx.PaymentDetails.CurrencyCode, tx.Date.Value, EnumMap[tx.Status.Value]),
                    tx => $"Missing info here: {tx.Id}, {tx.PaymentDetails.Amount} {tx.PaymentDetails.CurrencyCode} {tx.Date} {tx.Status}");
            }
            catch (Exception ex)
            {
                return new ParserResult(ex.Message);
            }
        }
    }

    [XmlRoot("Transactions")]
    public class XMLTransactions
    {
        [XmlElement("Transaction")]
        public XMLTransaction[] Transactions;
    }

    public class XMLTransaction
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlElement("TransactionDate")]
        public DateTime? Date { get; set; }

        [XmlElement("PaymentDetails")]
        public PaymentDetails PaymentDetails { get; set; }

        [XmlElement("Status")]
        public XMLTransactionStatus? Status { get; set; }

        public bool IsValid => !string.IsNullOrEmpty(Id)
            && Date.HasValue
            && PaymentDetails.IsValid
            && Status.HasValue;
    }

    public class PaymentDetails
    {
        [XmlElement("Amount")]
        public decimal Amount { get; set; }

        [XmlElement("CurrencyCode")]
        public string CurrencyCode { get; set; }

        public bool IsValid => Amount > 0 && !string.IsNullOrEmpty(CurrencyCode);
    }

    public enum XMLTransactionStatus
    {
        [XmlEnum("")] None = 0,
        [XmlEnum("Approved")] Approved = 1,
        [XmlEnum("Rejected")] Rejected = 2,
        [XmlEnum("Done")] Done = 3
    }
}
