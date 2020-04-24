using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using N8T.Infrastructure.Status;

namespace CoolStore.InventoryApi.Apis.Rest
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
    }
}
