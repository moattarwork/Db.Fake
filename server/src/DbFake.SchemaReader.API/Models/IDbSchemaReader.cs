using System.Collections.Generic;
using System.Threading.Tasks;
using DbFake.SchemaReader.API.Models.Domain;
using DbFake.SchemaReader.API.Services;

namespace DbFake.SchemaReader.API.Models
{
    public interface IDbSchemaReader
    {
        Task<IEnumerable<Database>> GetDatabasesAsync(ConnectionInfo connectionInfo);
        Task<IEnumerable<Table>> GetDatabaseTablesAsync(ConnectionInfo connectionInfo, string databaseName);
    }
}