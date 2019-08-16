using System.Threading.Tasks;
using Convey.CQRS.Commands;

namespace NPost.Services.Deliveries.Application.Commands.Handlers
{
    public class StartDeliveryHandler : ICommandHandler<StartDelivery>
    {
        public async Task HandleAsync(StartDelivery command)
        {

        }
    }
}