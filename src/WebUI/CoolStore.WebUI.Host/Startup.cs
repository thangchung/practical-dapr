using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace CoolStore.WebUI.Host
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] {"application/octet-stream"});
            });

            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = "Cookies";
                    options.DefaultChallengeScheme = "oidc";
                })
                .AddCookie("Cookies")
                .AddOpenIdConnect("oidc", options =>
                {
                    options.Authority = GetTyeAppUrl(_config, "identity-api");
                    options.RequireHttpsMetadata = false;
                    options.GetClaimsFromUserInfoEndpoint = true;

                    options.ClientId = "webui";
                    options.ClientSecret = "mysecret";
                    options.ResponseType = "code";

                    options.SaveTokens = true;

                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    options.Scope.Add("offline_access");
                    options.Scope.Add("graph-api");

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = "name", RoleClaimType = "role"
                    };
                });

            services.AddHttpClient("GraphQLClient")
                .ConfigureHttpClient(client =>
                {
                    client.BaseAddress = new System.Uri($"{GetTyeAppUrl(_config, "graph-api")}/graphql");
                });

            services.AddGraphQLClient();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapFallbackToFile("index.html");
            });
        }

        public string GetTyeAppUrl(IConfiguration config, string appId)
        {
            if (config.GetValue<bool>("IsDev"))
            {
                if (appId == "identity-api")
                {
                    return config.GetValue<string>("IdentityUrl");
                }
                else if (appId == "graph-api")
                {
                    return config.GetValue<string>("GraphQLUrl");
                }
            }

            var host = config[$"service:{appId}:host"];
            var port = config[$"service:{appId}:port"];
            var url = $"http://{host}:{port}";
            return url;
        }
    }
}
