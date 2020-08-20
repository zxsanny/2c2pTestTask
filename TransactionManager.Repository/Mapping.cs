using LinqToDB.DataProvider.SapHana;
using LinqToDB.Mapping;
using TransactionManager.Common.Entities;

namespace TransactionsManager
{
    public class MappingSchemaBuilder
    {
        public MappingSchema BuildMapping()
        {
            var builder = MappingSchema.Default.GetFluentMappingBuilder();
            builder.Entity<TransactionInfo>().HasTableName("Transactions");

            return builder.MappingSchema;
        }            
    }
}