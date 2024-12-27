namespace PS.Services.Interfaces
{
    public interface IServiceBusMessageSender
    {
        Task SendMessageAsync(string message);

    }    
}