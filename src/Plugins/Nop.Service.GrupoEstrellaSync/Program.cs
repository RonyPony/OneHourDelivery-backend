using System;
using System.ServiceProcess;
using Microsoft.Extensions.Configuration;

namespace Nop.Service.GrupoEstrellaSync
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            IConfiguration Configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();
            //Service1 service = new Service1(Configuration);
            //if (Environment.UserInteractive)
            //{
            //    service.RunAsConsole(args);
            //}
            //else
            //{
            //    ServiceBase[] ServicesToRun;
            //    ServicesToRun = new ServiceBase[]
            //    {
            //        new Service1(Configuration)
            //    };
            //    ServiceBase.Run(ServicesToRun);
            //}
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service1(Configuration)
            };
            ServiceBase.Run(ServicesToRun);
        }
    }

    // Startup.cs
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
    }
}