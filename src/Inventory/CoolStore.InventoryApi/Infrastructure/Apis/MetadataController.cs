using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using N8T.Infrastructure.Status;

namespace CoolStore.InventoryApi.Infrastructure.Apis
{
    [ApiController]
    [Route("")]
    public class MetadataController : ControllerBase
    {
        [HttpGet("/status")]
        public IActionResult Status([FromServices] IConfiguration config)
        {
            return Content(config.BuildAppStatus());
        }
    }
}
