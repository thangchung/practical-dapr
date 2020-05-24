using System;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace CoolStore.WebUI.Host
{
    public static class Extensions
    {
        public static string GRAPH_API_ID = "graphapi";
        public static string IDENTITY_API_ID = "identityserver";

        public static string GetTyeAppUrl(this IConfiguration config, string appId)
        {
            if (config.GetValue<bool>("NoTye"))
            {
                if (appId == IDENTITY_API_ID)
                {
                    return config.GetValue<string>("IdentityUrl");
                }
                else if (appId == GRAPH_API_ID)
                {
                    return config.GetValue<string>("GraphQLUrl");
                }
            }

            var host = config[$"service:{appId}:host"];
            var port = config[$"service:{appId}:port"];
            var url = $"http://{host}:{port}";
            return url;
        }

        [DebuggerStepThrough]
        public static T ConvertTo<T>(this object input)
        {
            return ConvertTo<T>(input.ToString());
        }

        [DebuggerStepThrough]
        public static T ConvertTo<T>(this string input)
        {
            try
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                return (T) converter.ConvertFromString(input);
            }
            catch (NotSupportedException)
            {
                return default;
            }
        }
    }
}
