using System;
using System.Threading.Tasks;
using Convey.CQRS.Events;
using Microsoft.Extensions.Logging;
using NPost.Services.Deliveries.Core.Entities;
using NPost.Services.Deliveries.Core.Repositories;

namespace NPost.Services.Deliveries.Application.Events.Handlers
{
    public class ParcelAddedHandler : IEventHandler<ParcelAdded>
    {
        private readonly ILogger<ParcelAddedHandler> _logger;
        private readonly IParcelsRepository _parcelsRepository;

        public ParcelAddedHandler(ILogger<ParcelAddedHandler> logger, IParcelsRepository parcelsRepository)
        {
            _logger = logger;
            _parcelsRepository = parcelsRepository;
        }

        public async Task HandleAsync(ParcelAdded parceAddedEvent)
        {
            _logger.LogInformation($"Parcel added event: {parceAddedEvent.ParcelId}");
            await _parcelsRepository.AddAsync(new Parcel(parceAddedEvent.ParcelId, string.Empty));
        }
    }
}