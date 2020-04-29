using System;
using System.Threading.Tasks;
using Dapr;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using N8T.Infrastructure.Status;

namespace CoolStore.InventoryApi.Infrastructure.Apis.Rest
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet("/status")]
        public IActionResult Status([FromServices] IConfiguration config)
        {
            return Content(config.BuildAppStatus());
        }

        [Topic("productCreated")]
        [HttpPost("productCreated")]
        public Task SubcribeProductCreated(ProductCreated product, [FromServices] DaprClient daprClient)
        {
            return Task.CompletedTask;
        }
    }

    public class ProductCreated
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public string ImageUrl { get; set; } = "https://picsum.photos/1200/900?image=1";
        public string? Description { get; set; }
        public DateTime CreatedAt { get; }
    }
}
