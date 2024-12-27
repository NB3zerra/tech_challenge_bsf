using PS.Domain.DTOs;
using PS.Domain.Entities;

namespace PS.Services.Interfaces
{
    public interface IPaymentProcessingService
    {
        PaymentIntentDto? DeserializePaymentIntentMessage(string message);
        Task ProcessPaymentIntent(PaymentIntentEntity paymentIntent);
    }
}