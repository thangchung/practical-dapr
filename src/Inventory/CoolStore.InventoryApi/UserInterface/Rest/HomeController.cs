using Microsoft.AspNetCore.Mvc;

namespace CoolStore.InventoryApi.UserInterface.Rest
{
    [ApiController]
    [Route("/home")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return new JsonResult("home");
        }
    }
}
