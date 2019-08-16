using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Microsoft.Extensions.Logging;

namespace NPost.Services.Deliveries.Application.Commands.Handlers
{
    public class StartDeliveryHandler : ICommandHandler<StartDelivery>
    {
        private readonly ILogger<StartDeliveryHandler> _logger;

        public StartDeliveryHandler(ILogger<StartDeliveryHandler> logger)
        {
            _logger = logger;
        }

        public Task HandleAsync(StartDelivery command)
        {
            _logger.LogInformation($"Starting a delivery {command.DeliveryId}");
            return Task.CompletedTask;
        }
    }
}