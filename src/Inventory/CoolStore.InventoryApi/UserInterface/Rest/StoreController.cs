using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoolStore.InventoryApi.Application.UseCases.GetAvailabilityInventories;
using CoolStore.Protobuf.Inventory.V1;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoolStore.InventoryApi.UserInterface.Rest
{
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<StoreController> _logger;

        public StoreController(
            IMediator mediator,
            ILogger<StoreController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("/stores")]
        public async Task<ActionResult<List<InventoryDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetInventoriesQuery());
            _logger.LogInformation($"Got {result.ToList().Count} items.");
            var inventories = new List<InventoryDto>();
            inventories.AddRange(result);
            return inventories;
        }
    }
}
