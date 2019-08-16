using System;
using System.Threading.Tasks;
using Convey.CQRS.Events;
using Microsoft.Extensions.Logging;

namespace NPost.Services.Deliveries.Application.Events.Handlers
{
    public class ParcelAddedHandler : IEventHandler<ParcelAdded>
    {
        private readonly ILogger<ParcelAddedHandler> _logger;

        public ParcelAddedHandler(ILogger<ParcelAddedHandler> logger)
        {
            _logger = logger;
        }

        public async Task HandleAsync(ParcelAdded parceAddedEvent)
        {
            _logger.LogInformation($"Parcel added event: {parceAddedEvent.ParcelId}");
            return;
        }
    }
}