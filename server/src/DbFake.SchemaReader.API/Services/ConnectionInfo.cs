using System;

namespace DbFake.SchemaReader.API.Services
{
    public sealed class ConnectionInfo
    {
        public string ConnectionString { get; }

        public ConnectionInfo(string connectionString)
        {
            ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }
    }
}