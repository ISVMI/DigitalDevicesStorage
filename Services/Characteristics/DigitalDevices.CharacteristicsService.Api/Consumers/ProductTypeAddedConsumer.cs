using DigitalDevices.CharacteristicsService.Application.Interfaces;
using MassTransit;
using Shared.Messages;

namespace DigitalDevices.CharacteristicsService.Api.Consumers
{
    public class ProductTypeAddedConsumer : IConsumer<NewProductTypeAdded>
    {
        private readonly ICharacteristicsTypeProductTypesService _service;

        public ProductTypeAddedConsumer(ICharacteristicsTypeProductTypesService service)
        {
            _service = service;
        }
        public async Task Consume(ConsumeContext<NewProductTypeAdded> context)
        {

            Console.WriteLine($"--> Adding relations...");

            await _service.AddNewRelation(context.Message.ProductTypeId, context.Message.CharacteristicsIds);

            Console.WriteLine($"--> Relations successfully added!");

        }
    }
}
