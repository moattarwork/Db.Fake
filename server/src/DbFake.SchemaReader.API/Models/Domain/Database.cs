using System;

namespace DbFake.SchemaReader.API.Models.Domain
{
    public sealed class Database
    {
        public string Name { get; }
        public Database(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}