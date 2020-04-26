using System.Net;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using N8T.Infrastructure.Tye;

namespace N8T.Infrastructure.Kestrel
{
    public static class Extensions
    {
        public static KestrelServerOptions ListenHttpAndGrpcProtocols(
            this KestrelServerOptions options,
            IConfiguration config,
            string appId)
        {
            options.Limits.MinRequestBodyDataRate = null;

            options.Listen(IPAddress.Any, config.GetHttpPort(appId));

            options.Listen(IPAddress.Any,
                config.GetGrpcPort(appId),
                listenOptions => { listenOptions.Protocols = HttpProtocols.Http2; });

            return options;
        }
    }
}
