using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Microsoft.Extensions.Logging;
using NPost.Services.Deliveries.Core.Entities;
using NPost.Services.Deliveries.Core.Repositories;

namespace NPost.Services.Deliveries.Application.Commands.Handlers
{
    public class StartDeliveryHandler : ICommandHandler<StartDelivery>
    {
        private readonly ILogger<StartDeliveryHandler> _logger;
        private readonly IDeliveriesRepository _deliveriesRepository;

        public StartDeliveryHandler(ILogger<StartDeliveryHandler> logger, IDeliveriesRepository deliveriesRepository)
        {
            _logger = logger;
            _deliveriesRepository = deliveriesRepository;
        }

        public async Task HandleAsync(StartDelivery command)
        {
            _logger.LogInformation($"Starting a delivery {command.DeliveryId}");
            await _deliveriesRepository.AddAsync(new Delivery(command.DeliveryId));
            _logger.LogInformation("Saved delivery.");
        }
    }
}