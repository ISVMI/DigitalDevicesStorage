using DigitalDevices.CharacteristicsService.Core.Models;
using DigitalDevices.CharacteristicsService.Infrastructure.Data;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Messages;

namespace DigitalDevices.CharacteristicsService.Infrastructure.Consumers
{
    public class ProductCreatedConsumer : IConsumer<ProductCreated>
    {
        private readonly CharacteristicsContext _context;
        private readonly ILogger _logger;

        public ProductCreatedConsumer(CharacteristicsContext context, ILogger<ProductCreatedConsumer> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<ProductCreated> context)
        {
            _logger.LogInformation($"--> Adding relations for product with id: {context.Message.ProductId}");

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                foreach (var dto in context.Message.Characteristics)
                {
                    var characteristic = new Characteristics
                    {
                        CharacteristicsTypeId = dto.CharacteristicTypeId,
                        Value = dto.Value
                    };

                    _context.Characteristics.Add(characteristic);

                    var relation = new CharacteristicProduct
                    {
                        ProductId = context.Message.ProductId,
                        CharacteristicsId = characteristic.Id
                    };

                    _context.CharacteristicsProducts.Add(relation);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                _logger.LogInformation("--> Relations successfully added!");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, $"Failed to add relations for product {context.Message.ProductId}");
            }
        }
    }
}
