using Microsoft.Extensions.DependencyInjection;
using Nop.Service.AppIPOSSync.Helpers;
using System.ServiceProcess;

namespace Nop.Service.AppIPOSSync
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Set up dependency injection
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .ConfigureServices()
                .BuildServiceProvider();

            var service = serviceProvider.GetService<Service>();
            var servicesToRun = new ServiceBase[]
            {
                service
            };

            ServiceBase.Run(servicesToRun);
        }
    }
}
