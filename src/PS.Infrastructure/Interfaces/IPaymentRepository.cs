using PS.Domain.Entities;

namespace PS.Infrastructure.Interfaces
{
    public interface IPaymentRepository
    {
        Task<PaymentIntentEntity> GetByIdAsync(Guid id);
        Task SavePaymentIntentAsync (PaymentIntentEntity paymentIntent);
    }    
}