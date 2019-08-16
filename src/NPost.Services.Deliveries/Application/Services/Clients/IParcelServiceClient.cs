using System;
using System.Threading.Tasks;
using NPost.Services.Deliveries.Application.DTO;

namespace NPost.Services.Deliveries.Application.Services.Clients
{
    public interface IParcelServiceClient
    {
        Task<ParcelDto> GetAsync(Guid id);
    }
}