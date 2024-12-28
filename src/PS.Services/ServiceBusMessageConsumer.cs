using System.Text.Json;
using AutoMapper;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Hosting;
using PS.Domain.Entities;
using PS.Services.Interfaces;

namespace PS.Services
{
    public class ServiceBusMessageConsumer : BackgroundService
    {
        protected readonly ServiceBusClient _serviceBusClient;
        protected readonly IHostApplicationLifetime _hostApplicationLifetime;
        protected readonly IPaymentProcessingService _paymentProcessingService;
        protected readonly IMapper _mapper;
        private ServiceBusProcessor ServiceBusProcessor { get; set; }
        private PaymentIntentEntity PaymentIntentEntity { get; set; } = new();

        public ServiceBusMessageConsumer(
            IHostApplicationLifetime hostApplicationLifetime,
            IPaymentProcessingService paymentProcessigService,
            IMapper mapper,
            ServiceBusClient client
        )
        {
            _hostApplicationLifetime = hostApplicationLifetime;
            _mapper = mapper;
            _paymentProcessingService = paymentProcessigService;
            _serviceBusClient = client;

            this.ServiceBusProcessor = _serviceBusClient.CreateProcessor("payment_intent_queue");
        }

        public async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();

            try
            {
                // Simular um erro randômico
                if (new Random().Next(0, 2) == 0) // 50% de chance de erro
                {
                    throw new Exception("Erro randômico simulado no processamento.");
                }
                //deserializar a mensagem
                var paymentIntentMessageDto = _paymentProcessingService.DeserializePaymentIntentMessage(body);

                //criar entidade
                _mapper.Map(paymentIntentMessageDto, PaymentIntentEntity);

                //atualizar no banco
                await _paymentProcessingService.ProcessPaymentIntent(PaymentIntentEntity);
                Console.WriteLine($"Payment Intent Entity: {JsonSerializer.Serialize(PaymentIntentEntity)}");

                // Confirmar mensagem como processada
                await args.CompleteMessageAsync(args.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar mensagem: {ex.Message}");
            }
        }

        public static Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception);
            return Task.CompletedTask;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                if (ServiceBusProcessor != null)
                {
                    ServiceBusProcessor.ProcessMessageAsync += MessageHandler;

                    ServiceBusProcessor.ProcessErrorAsync += ErrorHandler;

                    await ServiceBusProcessor.StartProcessingAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR| Messaging service error: {ex.Message}");
                throw;
            }
        }
    }
}