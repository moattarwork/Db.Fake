using System;
using System.Threading.Tasks;
using DbFake.SchemaReader.API.Services;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DbFake.Executor.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SchemaController : ControllerBase
    {
        private readonly ILogger<SchemaController> _logger;
        private readonly Clients _client;

        public SchemaController(ILogger<SchemaController> logger, IOptions<Clients> optionsAccessor)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _client = optionsAccessor?.Value ?? throw new ArgumentNullException(nameof(optionsAccessor));
        }

        [HttpGet]
        public async Task<IActionResult> GetDatabases(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                return BadRequest("Invalid connection string");

            using var channel = GrpcChannel.ForAddress(_client.SchemaReaderApiUrl);
            var schemaClient =  new Schema.SchemaClient(channel);
            var reply = await schemaClient.GetDatabasesAsync(new GetDatabasesRequest {ConnectionString = connectionString});

            return Ok(reply.Databases);
        }
        
        [HttpGet("for/{database}")]
        public async Task<IActionResult> GetDatabase(string connectionString, string database)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                return BadRequest("Invalid connection string");
        
            using var channel = GrpcChannel.ForAddress(_client.SchemaReaderApiUrl);
            var schemaClient =  new Schema.SchemaClient(channel);
            var reply = await schemaClient.GetDatabaseAsync(new GetDatabaseRequest {ConnectionString = connectionString, Database = database});
        
            return Ok(reply.Tables);
        }
    }
}