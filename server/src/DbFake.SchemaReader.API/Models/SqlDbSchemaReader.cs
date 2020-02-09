using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DbFake.SchemaReader.API.Models.Domain;
using DbFake.SchemaReader.API.Services;

namespace DbFake.SchemaReader.API.Models
{
    public class SqlDbSchemaReader : IDbSchemaReader
    {
        public async Task<IEnumerable<Database>> GetDatabasesAsync(ConnectionInfo connectionInfo)
        {
            await using var connection = new SqlConnection(connectionInfo.ConnectionString);
            return await connection.QueryAsync<Database>("SELECT name FROM SYS.Databases WHERE name not in ('master','tempdb','model','msdb')");
        }        
        
        public async Task<IEnumerable<Table>> GetDatabaseTablesAsync(ConnectionInfo connectionInfo, string databaseName)
        {
            await using var connection = new SqlConnection($"{connectionInfo.ConnectionString}; Initial Catalog={databaseName}");

            var query = "SELECT t.TABLE_SCHEMA SchemaName, t.TABLE_NAME TableName, " +
                        "COLUMN_NAME ColumnName, DATA_TYPE DataType, CHARACTER_MAXIMUM_LENGTH DataTypeLength, IS_NULLABLE isNullable " +
                        "FROM INFORMATION_SCHEMA.TABLES t JOIN INFORMATION_SCHEMA.COLUMNS c " +
                        "ON t.TABLE_SCHEMA = c.TABLE_SCHEMA AND t.TABLE_NAME = c.TABLE_NAME " +
                        "ORDER BY ORDINAL_POSITION";

            var schema =  await connection.QueryAsync<TableSchema>(query);

            return schema.GroupBy(m => new {m.SchemaName, m.TableName})
                .Select(g =>
                {
                    var fields =
                        g.Select(f => new Field(f.ColumnName, f.DataType, f.DataTypeLength, f.IsNullable == "YES"))
                            .ToArray();
                    return new Table(g.Key.SchemaName, g.Key.TableName, fields);
                }).ToArray();
        }        
       
        public class TableSchema
        {
            public string SchemaName { get; set; }
            public string TableName { get; set; }
            public string ColumnName { get; set; }
            public string DataType { get; set; }
            public int DataTypeLength { get; set; }
            public string IsNullable { get; set; }
        }
    }
}