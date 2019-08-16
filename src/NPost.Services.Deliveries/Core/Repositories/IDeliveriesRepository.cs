using System;
using System.Threading.Tasks;
using NPost.Services.Deliveries.Core.Entities;

namespace NPost.Services.Deliveries.Core.Repositories
{
    public interface IDeliveriesRepository
    {
        Task AddAsync(Delivery delivery);
        Task UpdateAsync(Delivery delivery);
        Task<Delivery> GetAsync(Guid id);
    }
}