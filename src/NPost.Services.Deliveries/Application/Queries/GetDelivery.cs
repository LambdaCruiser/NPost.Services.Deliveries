using System;
using Convey.CQRS.Queries;
using NPost.Services.Deliveries.Core.Entities;

namespace NPost.Services.Deliveries.Application.Queries
{
    public class GetDelivery : IQuery<Delivery>
    {
        public Guid DeliveryId { get; set; }
    }
}
