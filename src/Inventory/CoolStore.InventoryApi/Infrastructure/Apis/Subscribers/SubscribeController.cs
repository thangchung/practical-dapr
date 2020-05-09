using System.Threading.Tasks;
using CoolStore.InventoryApi.Application.AddStoreProductPrice;
using Dapr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoolStore.InventoryApi.Infrastructure.Apis.Subscribers
{
    [ApiController]
    [Route("")]
    public class SubscribeController : ControllerBase
    {
        [Topic("product-created")]
        [HttpPost("product-created")]
        public async Task SubcribeProductCreated(
            AddStoreProductPriceCommand command,
            [FromServices] IMediator mediator)
        {
            await mediator.Send(command);
        }

        [Topic("product-updated")]
        [HttpPost("product-updated")]
        public async Task SubcribeProductUpdated(
            AddStoreProductPriceCommand command,
            [FromServices] IMediator mediator)
        {
            await mediator.Send(command);
        }

        [Topic("product-deleted")]
        [HttpPost("product-deleted")]
        public async Task SubcribeProductDeleted(
            AddStoreProductPriceCommand command,
            [FromServices] IMediator mediator)
        {
            await mediator.Send(command);
        }
    }
}
