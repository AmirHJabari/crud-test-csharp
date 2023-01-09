using BoDi;
using Ductus.FluentDocker;
using Ductus.FluentDocker.Builders;
using Ductus.FluentDocker.Commands;
using Ductus.FluentDocker.Common;
using Ductus.FluentDocker.Extensions;
using Ductus.FluentDocker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Client.HttpClients;

namespace AcceptanceTests.Hooks
{
    [Binding]
    public class DockerControllerHooks
    {
        static ICompositeService _compositeService;
        private IObjectContainer _objectContainer;

        public DockerControllerHooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeTestRun]
        public static async Task DockerComposeUp()
        {
            var config = LoadConfiguration();
            var dockerComposeFileName = config["DockerComposeFileName"];
            var dockerComposeOverrideFileName = config["DockerComposeOverrideFileName"];

            var dockerComposePath = GetDockerComposeLocation(dockerComposeFileName);
            var dockerComposeOverridePath = GetDockerComposeLocation(dockerComposeOverrideFileName);

            var confirmationUrl = config["WebApi:BaseAddress"];

            _compositeService = new Builder()
                    .UseContainer()
                    .UseCompose()
                    .FromFile(dockerComposePath, dockerComposeOverridePath)
                    .RemoveOrphans()
                    .Build()
                .Start();

            using var client = new HttpClient() { BaseAddress = new Uri(confirmationUrl) };
            for (int i = 0; i < 60; i++)
            {
                if (i > 60) throw new FluentDockerException("Failed to wait for webapi service");
                try
                {
                    var res = await client.GetAsync($"api/v1/Customers?PageNumber=1&PageSize=5");
                    var body = await res.Content.ReadAsStringAsync();
                    if (res.StatusCode == HttpStatusCode.OK && body.Contains("\"isSuccess\":true", StringComparison.Ordinal))
                        break;
                }
                catch { }
                finally
                {
                    await Task.Delay(1000);
                }
            }
        }

        [AfterTestRun]
        public static void DockerComposeDown()
        {
            _compositeService.Stop();
            _compositeService.Dispose();
        }

        [BeforeScenario]
        public void RegisterServices()
        {
            var config = LoadConfiguration();

            var client = new CustomerHttpClient(config["WebApi:BaseAddress"]);
            _objectContainer.RegisterInstanceAs(client);
        }

        static IConfiguration LoadConfiguration()
        {
            return new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"))
                .Build();
        }

        static string GetDockerComposeLocation(string dockerComposeFileName)
        {
            var directory = Directory.GetCurrentDirectory();
            while (!Directory.EnumerateFiles(directory, "*.yml").Any(s => s.EndsWith(dockerComposeFileName)))
            {
                directory = directory[..directory.LastIndexOf(Path.DirectorySeparatorChar)];
            }

            return Path.Combine(directory, dockerComposeFileName);
        }
    }
}
