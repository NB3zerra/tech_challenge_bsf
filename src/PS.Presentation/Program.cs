using AutoMapper;
using PS.Services;
using PS.Infrastructure.Settings;
using PS.Services.AutoMapper;
using PS.Services.Interfaces;
using System.Text.Json;
using Azure.Messaging.ServiceBus;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


void ConfigureServices(IServiceCollection services)
{
    services.AddSingleton(provider =>
    {
        var configuration = provider.GetRequiredService<IConfiguration>();
        var serviceBusSettings = configuration.GetSection("SenderSharedAccessKey").Get<ServiceBusSettings>();
        return new ServiceBusClient(serviceBusSettings!.ConnectionString);
    });

    services.AddSingleton<ServiceBusMessageProducer>();
    services.AddTransient<IPaymentRegistrationService, PaymentRegistrationService>();

    configAutoMapper(services);
}

void configAutoMapper(IServiceCollection services)
{
    var mapperConfig = new MapperConfiguration(cfg =>
    {
        cfg.AddProfile<PaymentProfile>();
    });

    var mapper = mapperConfig.CreateMapper();

    services.AddSingleton(mapper);
}

public partial class Program { }