using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PS.Infrastructure.Settings;

namespace PS.Services
{
    public class AzureCosmosDbService
    {
        private readonly IMongoDatabase _database;

        public AzureCosmosDbService(IOptions<CosmosDbSettings> cosmosDbSettings)
        {
            var settings = cosmosDbSettings.Value;

            if (string.IsNullOrWhiteSpace(settings.ConnectionString) || string.IsNullOrWhiteSpace(settings.DatabaseName))
            {
                throw new ArgumentException("CosmosDbSettings are not properly configured.");
            }

            var client = new MongoClient(settings.ConnectionString);
            _database = client.GetDatabase(settings.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            if (string.IsNullOrEmpty(collectionName))
            {
                throw new ArgumentNullException(nameof(collectionName));
            }

            return _database.GetCollection<T>(collectionName);
        }
    }
}
