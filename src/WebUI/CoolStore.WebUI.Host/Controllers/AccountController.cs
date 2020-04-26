using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace CoolStore.WebUI.Host.Controllers
{
    [ApiController]
    [Route("account")]
    public class AccountController : ControllerBase
    {
        [HttpGet("user")]
        public async Task<UserInfo> GetUser()
        {
            var loggedOutUser = new UserInfo {IsAuthenticated = false};
            if (User.Identity.IsAuthenticated)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                return new UserInfo {Name = User.Identity.Name, IsAuthenticated = true, AccessToken = accessToken};
            }

            return loggedOutUser;
        }

        [HttpGet("signin")]
        public IActionResult SignIn(string redirectUri)
        {
            if (string.IsNullOrEmpty(redirectUri) || !Url.IsLocalUrl(redirectUri))
            {
                redirectUri = "/";
            }

            return Challenge(new AuthenticationProperties {RedirectUri = redirectUri}, "oidc");
        }

        [HttpGet("signout")]
        public IActionResult SignOut()
        {
            return SignOut(new AuthenticationProperties {RedirectUri = "/"}, "Cookies", "oidc");
        }
    }
}
