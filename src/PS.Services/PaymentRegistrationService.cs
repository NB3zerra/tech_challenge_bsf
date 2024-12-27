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
        protected readonly ServiceBusMessageProducer _messageSender;

        public PaymentRegistrationService(IMapper mapper, ServiceBusMessageProducer messageSender)
        {
            _mapper = mapper;
            _messageSender = messageSender;
        }

        public async Task<PaymentIntentStatus> RegisterPaymentIntent(PaymentIntentDto paymentIntent)
        {
            paymentIntent.Status = PaymentIntentStatus.CREATED;
            paymentIntent.Uuid = Guid.NewGuid();

            await _messageSender.SendMessageAsync(JsonSerializer.Serialize(paymentIntent));

            return PaymentIntentStatus.CREATED;
        }
    }
}