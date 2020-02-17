using System;
using System.IO;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;
using DbFake.Executor.API.Models;
using DbFake.SchemaReader.API.Services;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace DbFake.Executor.API.Controllers
{
    // TODO: Improve proxy creation
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
        
        [HttpPost("export")]
        public async Task<IActionResult> ExportMaskPlan([FromBody] ExportRequest exportRequest)
        {
            if (exportRequest == null) throw new ArgumentNullException(nameof(exportRequest));

            var json = JsonConvert.SerializeObject(exportRequest);
            var stream = new MemoryStream();
            stream.Write(Encoding.UTF8.GetBytes(json));
            stream.Position = 0;

            Response.Headers["x-filename"] = $"export_{DateTime.Now.Ticks}.json";
            return File(stream, "application/octet-stream", $"export_{DateTime.Now.Ticks}.json");
        }
    }
}