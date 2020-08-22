using System.Threading.Tasks;
using CoolStore.InventoryApi.Application.AddStoreProductPrice;
using CoolStore.InventoryApi.Application.DeleteStoreProductPrice;
using CoolStore.InventoryApi.Application.UpdateStoreProductPrice;
using Dapr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoolStore.InventoryApi.Infrastructure.Apis.Subscribers
{
    [ApiController]
    [Route("")]
    public class SubscribeController : ControllerBase
    {
        [Topic("pubsub", "product-created")]
        [HttpPost("product-created")]
        public async Task SubcribeProductCreated(AddStoreProductPriceCommand command,
            [FromServices] IMediator mediator)
        {
            await mediator.Send(command);
        }

        [Topic("pubsub", "product-updated")]
        [HttpPost("product-updated")]
        public async Task SubcribeProductUpdated(UpdateStoreProductPriceCommand command,
            [FromServices] IMediator mediator)
        {
            await mediator.Send(command);
        }

        [Topic("pubsub", "product-deleted")]
        [HttpPost("product-deleted")]
        public async Task SubcribeProductDeleted(DeleteStoreProductPriceCommand command,
            [FromServices] IMediator mediator)
        {
            await mediator.Send(command);
        }
    }
}
