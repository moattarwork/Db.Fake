using System.Threading.Tasks;

namespace DbFake.SchemaReader.API.Services
{
    public class SchemaService : Schema.SchemaBase
    {
        public Task<GetDatabasesReply> GetDatabases(GetDatabasesRequest request)
        {
            return null;
        }        
        
        public Task<GetFieldsReply> GetFields(GetFieldsRequest request)
        {
            return null;
        }
    }
}