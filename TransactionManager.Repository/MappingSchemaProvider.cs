using LinqToDB.DataProvider.SapHana;
using LinqToDB.Mapping;
using System;
using TransactionManager.Common.Entities;

namespace TransactionsManager
{
    public sealed class MappingSchemaProvider
    {
        private MappingSchemaProvider() 
        {}

        public static MappingSchemaProvider Instance => lazy.Value;

        private static readonly Lazy<MappingSchemaProvider> lazy =
            new Lazy<MappingSchemaProvider>(() => new MappingSchemaProvider());

        
        public MappingSchema Schema => schema ?? (schema = BuildSchema());
        private MappingSchema schema;
        private MappingSchema BuildSchema()
        {
            var builder = MappingSchema.Default.GetFluentMappingBuilder();
            builder.Entity<TransactionInfo>().HasTableName("Transactions");

            return builder.MappingSchema;
        }
    }
}