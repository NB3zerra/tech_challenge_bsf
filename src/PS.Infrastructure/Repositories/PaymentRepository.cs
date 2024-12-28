using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PS.Domain.Entities;
using PS.Infrastructure.Interfaces;
using PS.Infrastructure.Settings;

namespace PS.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IMongoCollection<PaymentIntentEntity> _collection;

        public PaymentRepository(IMongoDatabase database, IOptions<CosmosDbSettings> cosmosDbSettings)
        {
            
            _collection = database.GetCollection<PaymentIntentEntity>(cosmosDbSettings.Value.CollectionName!);
        }

        public async Task<PaymentIntentEntity> GetByIdAsync(Guid id)
        {
            return await _collection.Find(p => p.Uuid == id).FirstOrDefaultAsync();
        }

        public async Task SavePaymentIntentAsync(PaymentIntentEntity paymentIntent)
        {
            await _collection.InsertOneAsync(paymentIntent);
        }
    }
}