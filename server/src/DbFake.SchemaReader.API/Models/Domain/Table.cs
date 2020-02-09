using System;

namespace DbFake.SchemaReader.API.Models.Domain
{
    public sealed class Table
    {
        public string SchemaName { get; }
        public string TableName { get; }
        public Field[] Fields { get; }

        public Table(string schemaName, string tableName, Field[] fields)
        {
            TableName = tableName ?? throw new ArgumentNullException(nameof(tableName));
            Fields = fields ?? throw new ArgumentNullException(nameof(fields));
            SchemaName = schemaName ?? throw new ArgumentNullException(nameof(schemaName));
        }
    }
}