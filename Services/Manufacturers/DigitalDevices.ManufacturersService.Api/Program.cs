using DigitalDevices.ManufacturersService.Application.Extensions;
using DigitalDevices.ManufacturersService.Application.Mapping;
using DigitalDevices.ManufacturersService.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddAutoMapper(typeof(ManufacturersProfile));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

Console.WriteLine($"Connection string: {builder.Configuration.GetConnectionString("DefaultConnection")}");

await app.Services.AddDatabaseInitialization();

app.Run();
