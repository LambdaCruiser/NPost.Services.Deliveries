using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using MongoDB.Driver;
using NPost.Services.Deliveries.Application.Queries;
using NPost.Services.Deliveries.Core.Entities;

namespace NPost.Services.Deliveries.Infrastructure.Mongo.Queries
{
    public class GetDeliveryHandler : IQueryHandler<GetDelivery, Delivery>
    {
        private readonly IMongoDatabase _database;

        public GetDeliveryHandler(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Delivery> HandleAsync(GetDelivery query)
        {
            var delivery = await _database.GetCollection<Delivery>("deliveries")
                .Find(p => p.Id == query.DeliveryId)
                .SingleOrDefaultAsync();

            return delivery;
        }
    }
}
