using DigitalDevices.ProductCatalogService.Application.Extensions;
using DigitalDevices.ProductCatalogService.Application.Interfaces;
using DigitalDevices.ProductCatalogService.Infrastructure.Clients;
using DigitalDevices.ProductCatalogService.Infrastructure.Data;
using DigitalDevices.ProductCatalogService.Infrastructure.Extensions;
using Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddApplication();

builder.Services.AddDatabaseService<ProductCatalogContext>(builder.Configuration);

builder.Services.AddHttpClient<IProductTypesClient, ProductTypesClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:ProductTypes"]);
});

builder.Services.AddHttpClient<IManufacturersClient, ManufacturersClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:Manufacturers"]);
});

builder.Services.AddHttpClient<ICharacteristicsClient, CharacteristicsClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:Characteristics"]);
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

app.Run();
