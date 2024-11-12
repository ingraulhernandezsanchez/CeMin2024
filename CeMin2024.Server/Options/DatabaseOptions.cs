namespace CeMin2024.Server.Options
{
    public class DatabaseOptions
    {
        public int CommandTimeout { get; set; }
        public int MaxRetryAttempts { get; set; }
        public bool EnableDetailedErrors { get; set; }
    }
}