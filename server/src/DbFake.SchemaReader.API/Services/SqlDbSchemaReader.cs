using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace DbFake.SchemaReader.API.Services
{
    public class SqlDbSchemaReader : IDbSchemaReader
    {
        public async Task<IEnumerable<Database>> GetDatabasesAsync(ConnectionInfo connectionInfo)
        {
            await using var connection = new SqlConnection(connectionInfo.ConnectionString);
            var schema = await connection.QueryAsync<TableSchema>("SELECT TABLE_CATALOG AS DatabaseName, TABLE_SCHEMA AS SchemaName, TABLE_NAME AS TableName FROM INFORMATION_SCHEMA.TABLES");

            return schema.GroupBy(s => s.DatabaseName)
                .Select(g =>
                {
                    var name = g.Key;
                    var tables = g.Select(t => new Table($"[{t.SchemaName}].[{t.TableName}]")).ToArray();

                    return new Database(name, tables);
                }).ToArray();
        }

        public class TableSchema
        {
            public string TableName { get; set; } 
            public string SchemaName { get; set; } 
            public string DatabaseName { get; set; } 
        }
    }

    public interface IDbSchemaReader
    {
        Task<IEnumerable<Database>> GetDatabasesAsync(ConnectionInfo connectionInfo);
    }

    public sealed class ConnectionInfo
    {
        public string ConnectionString { get; }

        public ConnectionInfo(string connectionString)
        {
            ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }
    }

    public sealed class Database
    {
        public string Name { get; }
        public Table[] Tables { get; }

        public Database(string name, Table[] tables)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Tables = tables ?? throw new ArgumentNullException(nameof(tables));
        }
    }

    public sealed class Table
    {
        public string Name { get; }

        public Table(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }

    public sealed class Field
    {
        public string Name { get; }
        public string Type { get; }

        public Field(string name, string type)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }
    }
}