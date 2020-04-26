using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace N8T.Infrastructure.Tye
{
    public static class Extensions
    {
        public static void AddTyeBindingSecrets(this IConfigurationBuilder config)
        {
            if (!Directory.Exists("/var/tye/bindings/"))
                return;
            foreach (var directory in Directory.GetDirectories("/var/tye/bindings/"))
            {
                Console.WriteLine($"Adding config in '{directory}'.");
                config.AddKeyPerFile(directory, optional: true);
            }
        }

        public static string GetTyeAppUrl(this IConfiguration config, string appId)
        {
            var host = config[$"service:{appId}:host"];
            var port = config[$"service:{appId}:port"];
            var url = $"http://{host}:{port}";
            return url;
        }

        public static string GetTyeGrpcAppUrl(this IConfiguration config, string appId)
        {
            var host = config[$"service:{appId}:https:host"];
            var port = config[$"service:{appId}:https:port"];
            var url = $"http://{host}:{port}"; // insecure mode - https termination
            return url;
        }

        public static int GetHttpPort(this IConfiguration config, string appId)
        {
            return config[$"service:{appId}:port"].ConvertTo<int>();
        }

        public static int GetGrpcPort(this IConfiguration config, string appId)
        {
            return config[$"service:{appId}:https:port"].ConvertTo<int>();
        }

        public static string GetTyeSqlServerConnString(this IConfiguration config, string appId, string dbName, string dbPassword = "P@ssw0rd")
        {
            var connString = config[$"connectionstring:{appId}"] ??
                             $"Data Source={config["service:" + appId + ":host"]},{config["service:" + appId + ":port"]};Initial Catalog={dbName};User Id=sa;Password={dbPassword};MultipleActiveResultSets=True;";
            return connString;
        }
    }
}
