namespace PS.Infrastructure.Settings
{
    public class CosmosDbSettings
    {
        public string? ConnectionString { get; set; }
        public string? DatabaseName { get; set; }
        public string? CollectionName { get; set; }
    }
}