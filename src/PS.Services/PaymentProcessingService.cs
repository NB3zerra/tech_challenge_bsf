using System.Text.Json;
using PS.Domain.DTOs;
using PS.Domain.Entities;
using PS.Domain.Common;
using PS.Infrastructure.Settings;
using PS.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace PS.Services
{
    public class PaymentProcessingService : IPaymentProcessingService
    {

        private readonly AzureCosmosDbService _cosmosDbService;
        private readonly string _collectionName;

        public PaymentProcessingService(AzureCosmosDbService azureCosmosDbService, IOptions<CosmosDbSettings> azureCosmosDbSettings)
        {
            _cosmosDbService = azureCosmosDbService;
            _collectionName = azureCosmosDbSettings.Value.CollectionName!;

            Console.WriteLine(JsonSerializer.Serialize(azureCosmosDbSettings.Value));
        }
        public PaymentIntentDto? DeserializePaymentIntentMessage(string message)
        {
            return JsonSerializer.Deserialize<PaymentIntentDto>(message);
        }

        public async Task ProcessPaymentIntent(PaymentIntentEntity paymentIntent)
        {
            try
            {
                paymentIntent.Status = PaymentIntentStatus.OK;
                paymentIntent.UpdatedAt = DateTime.Now;
                await SavePaymentIntentAsync(paymentIntent);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private async Task SavePaymentIntentAsync(PaymentIntentEntity paymentIntent)
        {
            var collection = _cosmosDbService.GetCollection<PaymentIntentEntity>(_collectionName);
            await collection.InsertOneAsync(paymentIntent);
        }
    }
}