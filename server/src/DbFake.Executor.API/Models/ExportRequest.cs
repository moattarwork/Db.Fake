namespace DbFake.Executor.API.Models
{
    public sealed class ExportRequest
    {
        public string Connection { get; set; }
        public string Database { get; set; }

        public MaskedField[] MaskedFields { get; set; }
    }
}