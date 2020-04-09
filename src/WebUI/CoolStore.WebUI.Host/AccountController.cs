using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace CoolStore.WebUI.Host
{
    public class AccountController : Controller
    {
        private static UserInfo LoggedOutUser = new UserInfo { IsAuthenticated = false };

        [HttpGet("account/user")]
        public async Task<UserInfo> GetUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                return new UserInfo { Name = User.Identity.Name, IsAuthenticated = true, AccessToken = accessToken };
            }

            return LoggedOutUser;
        }

        [HttpGet("account/signin")]
        public IActionResult SignIn(string redirectUri)
        {
            if (string.IsNullOrEmpty(redirectUri) || !Url.IsLocalUrl(redirectUri))
            {
                redirectUri = "/";
            }

            return Challenge(new AuthenticationProperties { RedirectUri = redirectUri }, "oidc");
        }

        [HttpGet("account/signout")]
        public IActionResult SignOut()
        {
            return SignOut(new AuthenticationProperties { RedirectUri = "/" }, "Cookies", "oidc");
        }
    }
}
