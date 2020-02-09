using System;

namespace DbFake.SchemaReader.API.Models.Domain
{
    public sealed class Field
    {
        public string Name { get; }
        public string Type { get; }
        public int? TypeLength { get; }
        public bool IsNullable { get; }

        public Field(string name, string type, int? typeLength, bool isNullable)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Type = type ?? throw new ArgumentNullException(nameof(type));
            TypeLength = typeLength;
            IsNullable = isNullable;
        }
    }
}