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
    }
}
