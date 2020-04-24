using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CoolStore.InventoryApi.UserInterface.Rest
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        private readonly IConfiguration _config;

        public HomeController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet("/status")]
        public IActionResult Status()
        {
            var basePath = PlatformServices.Default.Application.ApplicationBasePath;

            var appName = PlatformServices.Default.Application.ApplicationName;
            var appVersion = PlatformServices.Default.Application.ApplicationVersion;

            var runtimeFramework = PlatformServices.Default.Application.RuntimeFramework;

            var envs = new Dictionary<string, object>();
            foreach (var env in _config.GetChildren()) envs.Add(env.Key, env.Key);

            dynamic model = new JObject();

            model!.OSArchitecture = !RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX)
                    ? "Linux or OSX"
                    : "Others"
                : "Windows";

            model!.OSDescription = RuntimeInformation.OSDescription;

            model!.ProcessArchitecture = RuntimeInformation.ProcessArchitecture == Architecture.Arm
                ? "Arm"
                : RuntimeInformation.ProcessArchitecture == Architecture.Arm64
                    ? "Arm64"
                    : RuntimeInformation.ProcessArchitecture == Architecture.X64
                        ? "x64"
                        : RuntimeInformation.ProcessArchitecture == Architecture.X86
                            ? "x86"
                            : "Others";

            model!.BasePath = basePath;
            model!.AppName = appName;
            model!.AppVersion = appVersion;
            model!.RuntimeFramework = runtimeFramework.ToString();
            model!.FrameworkDescription = RuntimeInformation.FrameworkDescription;
            model!.HostName = Dns.GetHostName();
            model!.IPAddress = Dns.GetHostAddresses(Dns.GetHostName())
                .Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                .Aggregate(" ", (a, b) => $"{a} {b}");
            model!.Envs =
                JsonConvert.SerializeObject(envs, new JsonSerializerSettings {Formatting = Formatting.Indented});

            return Content(model!.ToString());
        }
    }
}
