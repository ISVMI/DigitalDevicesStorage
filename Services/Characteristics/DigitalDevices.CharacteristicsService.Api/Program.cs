using DigitalDevices.CharacteristicsService.Api.Consumers;
using DigitalDevices.CharacteristicsService.Application.Extensions;
using DigitalDevices.CharacteristicsService.Application.Mapping;
using DigitalDevices.CharacteristicsService.Infrastructure.Extensions;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddAutoMapper(typeof(CharacteristicsProfile));

builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<ProductTypeAddedConsumer>();

    config.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost");

        cfg.ReceiveEndpoint("new-product-type-added", e =>
        {
            e.ConfigureConsumer<ProductTypeAddedConsumer>(context);
        });
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.Services.AddDatabaseInitialization();

app.Run();
