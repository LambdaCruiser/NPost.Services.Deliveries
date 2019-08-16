using System;
using System.Collections.Generic;
using System.Linq;
using NPost.Services.Deliveries.Core.Entities;

namespace NPost.Services.Deliveries.Application.DTO
{
    public class DeliveryDto
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public RouteDto Route { get; set; }
        public IEnumerable<ParcelInDeliveryDto> Parcels { get; set; }

        public DeliveryDto()
        {
        }

        public DeliveryDto(Delivery delivery)
        {
            Id = delivery.Id;
            Status = delivery.Status.ToString();
            Notes = delivery.Notes;
            Parcels = delivery.Parcels.Select(pid => new ParcelInDeliveryDto(pid));
            if (delivery.Route != null)
                Route = new RouteDto(delivery.Route);

        }
    }
}