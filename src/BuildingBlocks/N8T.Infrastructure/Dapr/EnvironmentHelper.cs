using System;

namespace N8T.Infrastructure.Dapr
{
    public static class EnvironmentHelper
    {
        public static int GetHttpPort(int defaultPort = 3500)
        {
            var httpPort = defaultPort;
            if (Environment.GetEnvironmentVariable("DAPR_HTTP_PORT") != null)
            {
                httpPort = Environment.GetEnvironmentVariable("DAPR_HTTP_PORT").ConvertTo<int>();
            }

            return httpPort;
        }

        public static int GetGrpcPort(int defaultPort = 90001)
        {
            var httpPort = defaultPort;
            if (Environment.GetEnvironmentVariable("DAPR_GRPC_PORT") != null)
            {
                httpPort = Environment.GetEnvironmentVariable("DAPR_GRPC_PORT").ConvertTo<int>();
            }

            return httpPort;
        }
    }
}
