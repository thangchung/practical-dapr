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

        public static string GetTyeSqlServerConnString(this IConfiguration config, string appId, string dbName, string dbPassword = "P@ssw0rd")
        {
            var connString = config[$"connectionstring:{appId}"] ??
                             $"Data Source={config["service:" + appId + ":host"]},{config["service:" + appId + ":port"]};Initial Catalog={dbName};User Id=sa;Password={dbPassword};MultipleActiveResultSets=True;";
            return connString;
        }
    }
}
