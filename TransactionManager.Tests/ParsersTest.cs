using System.IO;
using System.Linq;
using TransactionManager.Parsers;
using Xunit;

namespace TransactionManager.Tests
{
    public class ParsersTest
    {
        [Theory]
        [InlineData(@"
<Transactions>
    <Transaction id=""Inv00001"">
        <TransactionDate>2019-01-23T13:45:10</TransactionDate>
        <PaymentDetails>
            <Amount>200.00</Amount>
            <CurrencyCode>USD</CurrencyCode>
        </PaymentDetails>
        <Status>Done</Status>
    </Transaction>
    
    <Transaction id=""Inv00002"">
        <TransactionDate>2019-01-24T16:09:15</TransactionDate>
        <PaymentDetails>
            <Amount>10000.00</Amount>
            <CurrencyCode>EUR</CurrencyCode>
        </PaymentDetails>
        <Status>Rejected</Status>
    </Transaction>
</Transactions>", "xml", 2, 200, "Inv00002")]
        [InlineData(@"""Invoice0000001"",""1,000.00"", ""USD"", ""20/02/2019 12:33:16"", ""Approved""
""Invoice0000002"",""300.00"",""USD"",""21/02/2019 02:04:59"", ""Failed""", "csv", 2, 1000, "Invoice0000002")]
        public void TestParserSuccess(string input, string extension, int count, decimal firstAmount, string lastId)
        { 
            var res = new FileParserFactory().CreateTransactionFileParser(extension).ParseStream(ToStream(input));
            Assert.True(res.Success);
            Assert.Empty(res.Errors);
            Assert.Equal(count, res.Transactions.Count);
            Assert.Equal(firstAmount, res.Transactions.First().Amount);
            Assert.Equal(lastId, res.Transactions.Last().Id);
        }

        [Theory]
        [InlineData(@"
<Transactions>
    <Transaction id=""Inv00001"">
        <TransactionDate>2019-01-23T13:45:10</TransactionDate>
        <PaymentDetails>
            <Amount>200.00</Amount>
            <CurrencyCode>USD</CurrencyCode>
        </PaymentDetails>
        <Status>Done</Status>
    </Transaction>
    
    <Transaction id=""Inv00002"">
        <TransactionDate>2019-01-24T16:09:15</TransactionDate>
        <PaymentDetails>
            <Amount>10000.00</Amount>
            <CurrencyCode></CurrencyCode>
        </PaymentDetails>
        <Status>Rejected</Status>
    </Transaction>
</Transactions>", "xml")]
        [InlineData(@"""Invoice0000001"",""1,000.00"", ""USD"", ""20/02/2019 12:33:16"", ""Approved""
""Invoice0000002"",""300.00"",""USD"",""21/02/2019 02:04:59"", """"", "csv")]
        public void ParserShouldErrorOnEmpty(string input, string extension)
        {
            var res = new FileParserFactory().CreateTransactionFileParser(extension).ParseStream(ToStream(input));
            Assert.False(res.Success);
            Assert.Single(res.Errors);
        }

        private Stream ToStream(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

    }
}
