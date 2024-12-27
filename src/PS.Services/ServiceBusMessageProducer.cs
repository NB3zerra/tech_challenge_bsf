using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;
using PS.Infrastructure.Settings;
using PS.Services.Interfaces;

namespace PS.Services
{
    public class ServiceBusMessageProducer : IServiceBusMessageSender
    {
        protected readonly ServiceBusClient _serviceBusClient;
        public ServiceBusMessageProducer(ServiceBusClient serviceBusClient)
        {
            _serviceBusClient = serviceBusClient;
        }

        public async Task SendMessageAsync(string message)
        {
            var sender = _serviceBusClient.CreateSender("payment_intent_queue");

            try
            {
                // Criar a mensagem
                var serviceBusMessage = new ServiceBusMessage(message);

                // Enviar a mensagem
                await sender.SendMessageAsync(serviceBusMessage);

                Console.WriteLine($"Message sent to queue payment_intent_queue: {message}");
            }
            finally
            {
                // Fechar o sender
                await sender.DisposeAsync();
            }
        }

    }
}