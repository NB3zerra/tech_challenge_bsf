using PS.Domain.Common;
using PS.Domain.DTOs.Requests;
using PS.Domain.Entities;
namespace PS.Services.Interfaces
{
    public interface IPaymentRegistrationService
    {
        Task<PaymentIntentStatus> RegisterPaymentIntent(PaymentIntentEntity paymentIntent);
    }
}
