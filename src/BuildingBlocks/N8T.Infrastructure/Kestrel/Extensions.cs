using System.Net;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;

namespace N8T.Infrastructure.Kestrel
{
    public static class Extensions
    {
        public static KestrelServerOptions ListenHttpAndGrpcProtocols(
            this KestrelServerOptions options,
            IConfiguration config,
            string appId)
        {
            var serviceHttpUri = config.GetServiceUri(appId);
            var serviceHttpsUri = config.GetServiceUri(appId, "https");

            options.Limits.MinRequestBodyDataRate = null;

            options.Listen(IPAddress.Any, serviceHttpUri.Port);

            options.Listen(IPAddress.Any,
                serviceHttpsUri.Port,
                listenOptions => { listenOptions.Protocols = HttpProtocols.Http2; });

            return options;
        }
    }
}
