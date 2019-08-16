using System;
using System.Threading.Tasks;
using Convey.CQRS.Events;
using Convey.HTTP;
using Microsoft.Extensions.Logging;
using NPost.Services.Deliveries.Application.DTO;
using NPost.Services.Deliveries.Core.Entities;
using NPost.Services.Deliveries.Core.Repositories;

namespace NPost.Services.Deliveries.Application.Events.Handlers
{
    public class ParcelAddedHandler : IEventHandler<ParcelAdded>
    {
        private readonly ILogger<ParcelAddedHandler> _logger;
        private readonly IParcelsRepository _parcelsRepository;
        private readonly IHttpClient _client;

        public ParcelAddedHandler(ILogger<ParcelAddedHandler> logger, IParcelsRepository parcelsRepository, IHttpClient client)
        {
            _logger = logger;
            _parcelsRepository = parcelsRepository;
            _client = client;
        }

        public async Task HandleAsync(ParcelAdded parcelAddedEvent)
        {
            //TODO - nie chcemy hardkodowanej konfiguracji, chcemy DNSa
            var parcelDto = await _client.GetAsync<ParcelDto>($"http://localhost:5002/parcels/{parcelAddedEvent.ParcelId}");
            if (parcelDto is null)
            {
                throw new ArgumentException("Parcel not fonud");
            }

            var parcel = new Parcel(parcelAddedEvent.ParcelId, parcelDto.Address);

            _logger.LogInformation($"Parcel added event: {parcelAddedEvent.ParcelId}");

            await _parcelsRepository.AddAsync(parcel);
        }
    }
}