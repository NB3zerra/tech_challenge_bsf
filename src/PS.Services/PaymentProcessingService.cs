using System.Text.Json;
using PS.Domain.DTOs;
using PS.Domain.Entities;
using PS.Domain.Common;
using PS.Infrastructure.Settings;
using PS.Services.Interfaces;
using Microsoft.Extensions.Options;
using PS.Infrastructure.Interfaces;
using PS.Infrastructure.Repositories;

namespace PS.Services
{
    public class PaymentProcessingService : IPaymentProcessingService
    {

        protected readonly PaymentRepository _repository; 
        public PaymentProcessingService(PaymentRepository repository)
        {
            _repository = repository;
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
                await _repository.SavePaymentIntentAsync(paymentIntent);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task<PaymentIntentStatus?> GetPaymentIntentStatusAsync(Guid paymentIntentId)
        {
            var paymentIntent = await _repository.GetByIdAsync(paymentIntentId);
            if (paymentIntent == null)
                throw new KeyNotFoundException($"PaymentIntent with ID {paymentIntentId} not found.");

            return paymentIntent.Status;
        }
    }
}