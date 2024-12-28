using AutoMapper;
using PS.Services;
using PS.Infrastructure.Settings;
using PS.Services.AutoMapper;
using PS.Services.Interfaces;
using Azure.Messaging.ServiceBus;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using PS.Infrastructure.Interfaces;
using PS.Infrastructure.Repositories;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


// Configuração do Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Payment Processing API",
        Version = "v1",
        Description = "API para registro e consulta de PaymentIntents.",
        Contact = new OpenApiContact
        {
            Name = "Suporte",
            Email = "suporte@empresa.com",
            Url = new Uri("https://github.com/NB3zerra/tech_challenge_bsf")
        },
        License = new OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Payment Processing API v1");
        c.RoutePrefix = string.Empty;
    });
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

    services.Configure<CosmosDbSettings>(builder.Configuration.GetSection("CosmosDbSettings"));
    BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

    services.AddSingleton<IMongoClient>(
        sp =>
        {
            var settings = sp.GetRequiredService<IOptions<CosmosDbSettings>>().Value;

            if (string.IsNullOrWhiteSpace(settings.ConnectionString))
            {
                throw new ArgumentException("Connection string for MongoDB is not configured.");
            }

            return new MongoClient(settings.ConnectionString);
        }
    );

    services.AddSingleton(
        sp =>
        {
            var settings = sp.GetRequiredService<IOptions<CosmosDbSettings>>().Value;

            if (string.IsNullOrWhiteSpace(settings.DatabaseName))
            {
                throw new ArgumentException("Database name for MongoDB is not configured.");
            }

            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(settings.DatabaseName);
        }
    );

    services.AddSingleton(provider =>
    {
        var configuration = provider.GetRequiredService<IConfiguration>();
        var serviceBusSettings = configuration.GetSection("SenderSharedAccessKey").Get<ServiceBusSettings>();
        return new ServiceBusClient(serviceBusSettings!.ConnectionString);
    });


    services.AddSingleton<ServiceBusMessageProducer>();

    services.AddSingleton<PaymentRepository>();
    services.AddSingleton<IPaymentProcessingService, PaymentProcessingService>();
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