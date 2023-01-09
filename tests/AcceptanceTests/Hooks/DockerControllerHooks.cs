using BoDi;
using Ductus.FluentDocker;
using Ductus.FluentDocker.Builders;
using Ductus.FluentDocker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public static void DockerComposeUp()
        {
            var config = LoadConfiguration();
            var dockerComposeFileName = config["DockerComposeFileName"];
            var dockerComposeOverrideFileName = config["DockerComposeOverrideFileName"];

            var dockerComposePath = GetDockerComposeLocation(dockerComposeFileName);
            var dockerComposeOverridePath = GetDockerComposeLocation(dockerComposeOverrideFileName);

            var confirmationUrl = config["WebApi:BaseAddress"];

            using var volume = new Builder().UseVolume("crudtest-tests-volume").RemoveOnDispose().Build();

            _compositeService = new Builder()
                    .UseContainer()
                    .UseCompose()
                    .FromFile(dockerComposePath, dockerComposeOverridePath)
                    .RemoveOrphans()
                    .WaitForHttp("webapi", $"{confirmationUrl}/health",
                        continuation: (response, _) => response.Code != System.Net.HttpStatusCode.OK ? 2000 : 0)
                    .Build()
                .Start();
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
