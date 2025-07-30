using DigitalDevices.CharacteristicsService.Core.Models;
using DigitalDevices.CharacteristicsService.Infrastructure.Data;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Messages;

namespace DigitalDevices.CharacteristicsService.Infrastructure.Consumers
{
    public class ProductTypeCreatedConsumer : IConsumer<ProductTypeCreated>
    {
        private readonly CharacteristicsContext _context;
        private readonly ILogger _logger;

        public ProductTypeCreatedConsumer(CharacteristicsContext context, ILogger<ProductTypeCreatedConsumer> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<ProductTypeCreated> context)
        {
            _logger.LogInformation($"--> Adding relations for product type with id: {context.Message.ProductTypeId}");

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                foreach (var characteristicTypeId in context.Message.CharacteristicTypesIds)
                {
                    var relation = new CharacteristicsTypeProductTypes
                    {
                        CharacteristicsTypeId = characteristicTypeId,
                        ProductTypesId = context.Message.ProductTypeId
                    };

                    await _context.CharacteristicsTypeProductTypes.AddAsync(relation);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                _logger.LogInformation("--> Relations successfully added!");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, $"Failed to add relations for product type {context.Message.ProductTypeId}");
            }
        }
    }
}
