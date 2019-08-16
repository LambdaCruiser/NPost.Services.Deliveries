using System;
using System.Threading.Tasks;
using Convey.HTTP;
using NPost.Services.Deliveries.Application.DTO;
using NPost.Services.Deliveries.Application.Services.Clients;

namespace NPost.Services.Deliveries.Infrastructure.Services.Clients
{
    public class ParcelServiceClient : IParcelServiceClient
    {
        private readonly IHttpClient _client;
        private readonly HttpClientOptions _options;
        private readonly string _url;

        public ParcelServiceClient(IHttpClient client, HttpClientOptions options)
        {
            _client = client;
            _options = options;
            _url = options.Services["parcels"];
        }

        public Task<ParcelDto> GetAsync(Guid id)
        {
            return _client.GetAsync<ParcelDto>($"{_url}/parcels/{id}");
        }
    }
}