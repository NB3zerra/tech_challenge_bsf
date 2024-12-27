using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;
using PS.Domain.Common;
using PS.Domain.DTOs;
using PS.Domain.Entities;
using PS.Infrastructure.Settings;
using PS.Services.Interfaces;

namespace PS.Services
{
    public class PaymentRegistrationService : IPaymentRegistrationService
    {
        protected readonly IMapper _mapper;
        protected readonly ServiceBusMessageSender _messageSender;

        public PaymentRegistrationService(IMapper mapper, ServiceBusMessageSender messageSender)
        {
            _mapper = mapper;
            _messageSender = messageSender;
        }

        public async Task<PaymentIntentStatus> RegisterPaymentIntent(PaymentIntentEntity paymentIntent)
        {
            var message = new PaymentIntentDto();

            paymentIntent.Status = PaymentIntentStatus.CREATED;

            _mapper.Map(paymentIntent, message);

            await _messageSender.SendMessageAsync(JsonSerializer.Serialize(message));

            return PaymentIntentStatus.CREATED;
        }
    }
}