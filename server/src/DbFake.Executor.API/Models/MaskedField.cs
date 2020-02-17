namespace DbFake.Executor.API.Models
{
    public sealed class MaskedField
    {
        public string TableName { get; set; }
        public string[] TableFields { get; set; }
    }
}