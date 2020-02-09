using System;
using System.Linq;
using System.Threading.Tasks;
using DbFake.SchemaReader.API.Models;
using Google.Protobuf.Collections;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace DbFake.SchemaReader.API.Services
{
    public class SchemaService : Schema.SchemaBase
    {
        private readonly IDbSchemaReader _dbSchemaReader;
        private readonly ILogger<SchemaService> _logger;

        public SchemaService(IDbSchemaReader dbSchemaReader, ILogger<SchemaService> logger)
        {
            _dbSchemaReader = dbSchemaReader ?? throw new ArgumentNullException(nameof(dbSchemaReader));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public override async Task<GetDatabasesReply> GetDatabases(GetDatabasesRequest request, ServerCallContext callContext)
        {
            var databases = await _dbSchemaReader.GetDatabasesAsync(new ConnectionInfo(request.ConnectionString));
            var reply = new GetDatabasesReply();
            reply.Databases.AddRange(databases.Select(d => new database() {Name = d.Name}));
            
            return reply;
        }        
        
        public override async Task<GetDatabaseReply> GetDatabase(GetDatabaseRequest request, ServerCallContext callContext)
        {
            var tables = await _dbSchemaReader.GetDatabaseTablesAsync(new ConnectionInfo(request.ConnectionString), request.Database);
            var reply = new GetDatabaseReply();
            reply.Tables.AddRange(tables.Select(d =>
            {
                var table = new table {Name = d.TableName, Schema = d.SchemaName};
                table.Fields.AddRange(d.Fields.Select(f => new field {Name = f.Name, Type = f.Type}));

                return table;
            }));
            
            return reply;
        }
    }

}