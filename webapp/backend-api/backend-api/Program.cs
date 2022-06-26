using backend_api.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using NLog.Web;
using backend_api.Data;
using System.Diagnostics;

namespace backend_api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //Configure logger
            var nlogConfig = new NLog.Config.LoggingConfiguration();
            var nlogFileTarget = new NLog.Targets.FileTarget("logfile") { FileName = "file.txt" };
            var nlogConsoleTarget = new NLog.Targets.ConsoleTarget("logconsole");
            nlogConfig.AddRuleForAllLevels(nlogConsoleTarget);
            //var logger = NLog.Web.NLogBuilder.ConfigureNLog(nlogConfig).GetCurrentClassLogger();
            var logger = NLogBuilder.ConfigureNLog("./nlog.config").GetCurrentClassLogger();

            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    logger.Debug("Init main...");
                    Debug.WriteLine("Init main");

                    var userManager = services.GetRequiredService<UserManager<User>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    //var userService = services.GetRequiredService<IUserService>();

                    await AppDbContextSeed.SeedEssentialsAsync(userManager, roleManager);
                    host.Run();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    logger.Error(ex, "Stopped program because of exception");
                }
                finally
                {
                    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                    NLog.LogManager.Shutdown();
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                    logging.AddConsole();
                })
                .UseNLog();  // NLog: Setup NLog for Dependency injection
    }
}