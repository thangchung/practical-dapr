using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

namespace CoolStore.WebUI
{
    public class ServerAuthStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;

        public ServerAuthStateProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var userInfo = await _httpClient.GetFromJsonAsync<UserInfo>("account/user");

            var identity = userInfo.IsAuthenticated
                ? new ClaimsIdentity(
                    new[]
                    {
                        new Claim("user_id", userInfo?.UserId), new Claim(ClaimTypes.Name, userInfo?.Name),
                        new Claim("access_token", userInfo?.AccessToken)
                    },
                    "serverauth")
                : new ClaimsIdentity();

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }
    }

    public class UserInfo
    {
        public bool IsAuthenticated { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public string AccessToken { get; set; }
    }
}
