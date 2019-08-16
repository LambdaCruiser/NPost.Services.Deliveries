using System;
using System.Collections.Generic;

namespace NPost.Services.Deliveries.Core.Entities
{
    public class Delivery
    {
        public Guid Id { get; private set; }
        public IEnumerable<ParcelInDelivery> Parcels { get; private set; }
        public Route Route { get; set; }
        public Status Status { get; set; }
        public string Notes { get; set; }

        public Delivery(Guid id, IEnumerable<ParcelInDelivery> parcels)
        {
            Id = id;
            Parcels = parcels;
            Notes = string.Empty;
        }
    }
}