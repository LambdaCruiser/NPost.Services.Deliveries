using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Microsoft.Extensions.Logging;
using NPost.Services.Deliveries.Application.Events;
using NPost.Services.Deliveries.Application.Services.Clients;
using NPost.Services.Deliveries.Core.Entities;
using NPost.Services.Deliveries.Core.Repositories;

namespace NPost.Services.Deliveries.Application.Commands.Handlers
{
    public class StartDeliveryHandler : ICommandHandler<StartDelivery>
    {
        private readonly ILogger<StartDeliveryHandler> _logger;
        private readonly IDeliveriesRepository _deliveriesRepository;
        private readonly IParcelsRepository _parcelsRepository;
        private readonly IMessageBroker _messageBroker;
        private readonly IRoutingServiceClient _routingServiceClient;

        public StartDeliveryHandler(ILogger<StartDeliveryHandler> logger, IDeliveriesRepository deliveriesRepository, 
            IParcelsRepository parcelsRepository, IMessageBroker messageBroker, IRoutingServiceClient routingServiceClient)
        {
            _logger = logger;
            _deliveriesRepository = deliveriesRepository;
            _parcelsRepository = parcelsRepository;
            _messageBroker = messageBroker;
            _routingServiceClient = routingServiceClient;
        }

        public async Task HandleAsync(StartDelivery command)
        {
            _logger.LogInformation($"Starting a delivery {command.DeliveryId}");

            if(command?.Parcels.Any() == null)
                throw new ArgumentException("Parcels are required");

            var parcels = new List<Parcel>();
            foreach (var parcelId in command.Parcels)
            {
                var parcel = await _parcelsRepository.GetAsync(parcelId);
                if(parcel == null) 
                    throw new ArgumentException($"Parcel {parcelId} does not exist");
                parcels.Add(parcel);
            }

            var route = await _routingServiceClient.GetAsync(parcels.Select(p => p.Address));

            var delivery = new Delivery(command.DeliveryId, parcels.Select(p => new ParcelInDelivery(p)), new Route(route.Addresses, route.TotalDistance));
            await _deliveriesRepository.AddAsync(delivery);
            _logger.LogInformation("Saved delivery.");

            foreach (var parcel in parcels)
            {
                await _messageBroker.PublishAsync(new DeliveryStarted(command.DeliveryId, parcel.Id));
            }
        }
    }
}