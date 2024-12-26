using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using PS.Domain.Common;
using PS.Domain.DTOs.Requests;
using PS.Domain.Entities;
using PS.Services.Interfaces;

namespace PS.Services
{
    public class PaymentRegistrationService : IPaymentRegistrationService
    {
        protected readonly IMapper _mapper;
        public PaymentRegistrationService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<PaymentIntentStatus> RegisterPaymentIntent(PaymentIntentEntity paymentIntent)
        {

            paymentIntent.Status = PaymentIntentStatus.CREATED;

            Console.WriteLine(JsonSerializer.Serialize(paymentIntent));

            return PaymentIntentStatus.CREATED;
        }
    }
}