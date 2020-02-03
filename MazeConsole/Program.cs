using System;
using System.Linq;
using MazeLib.Factory;
using MazeLib.Solvers;
using MazeLib.Utilities;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Threading.Tasks;

using System.Collections.Generic;

namespace MazeConsole
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builtHost = CreateHostBuilder(args).Build();

            var console = builtHost.Services.GetService<ConsoleApp>();
            await console.Run();
        }

         public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("settings.json", true, true);
                    config.AddCommandLine(args);
                })
                .ConfigureServices((hostcontext, services) =>
                {
                    services.AddTransient<ConsoleApp>();
                });
    }
}
