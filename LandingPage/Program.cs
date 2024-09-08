using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace LandingPage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Create and configure the logger
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()  // Set the minimum logging level
            .WriteTo.Console()     // Log to the console
            .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)  // Log to a file with daily rotation
            .CreateLogger();

            try
            {
                Log.Information("Starting the application...");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed.");
            }
            finally
            {
                Log.CloseAndFlush();  // Ensure all logs are written out
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseIISIntegration();
                });
    }
}
