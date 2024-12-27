using AutoMapper;
using PS.Services;
using PS.Infrastructure.Settings;
using PS.Services.AutoMapper;
using Azure.Messaging.ServiceBus;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson;

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

    services.Configure<CosmosDbSettings>(builder.Configuration.GetSection("CosmosDbSettings"));
    BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

    services.AddSingleton(provider =>
    {
        var configuration = provider.GetRequiredService<IConfiguration>();
        var serviceBusSettings = configuration.GetSection("ListenerSharedAccessKey").Get<ServiceBusSettings>();
        return new ServiceBusClient(serviceBusSettings!.ConnectionString);
    });

    services.AddSingleton<AzureCosmosDbService>();
    services.AddSingleton<PaymentProcessingService>();
    services.AddHostedService<ServiceBusMessageConsumer>();

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