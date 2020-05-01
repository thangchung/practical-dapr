using System.Threading.Tasks;
using CoolStore.InventoryApi.Application.AddStoreProductPrice;
using Dapr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoolStore.InventoryApi.Infrastructure.Apis.Subscribers
{
    [ApiController]
    [Route("")]
    public class SubscribeController
        : ControllerBase
    {
        [Topic("product-created")]
        [HttpPost("product-created")]
        public async Task SubcribeProductCreated(
            AddStoreProductPriceCommand command,
            [FromServices] IMediator mediator)
        {
            await mediator.Send(command);
        }
    }
}
