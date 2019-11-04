using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CrystalCards.Data;
using CrystalCards.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace CrystalCards.api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CustomInitLogic();
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                //NLog: catch setup errors
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }

         
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
            .ConfigureLogging(logging =>
        {
            logging.ClearProviders();
            logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
        })
        .UseNLog();  // NLog: setup NLog for Dependency injection

        private static async Task CustomInitLogic()
        {
            var directory = Directory.GetCurrentDirectory();
           
            if (File.Exists(directory+"/init.txt"))
            {
                var config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.Development.json", optional: false)
                    .Build();

                //create user 
                var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseSqlServer(config.GetConnectionString("CardDatabase"));
                
                var db = new ApplicationDbContext(dbContextOptions.Options);
                var userRepository=new AuthRepository(db);
                User user = new User {Username = "Admin"};
                user.Roles.Add(new CustomRole(){Name=Role.Administrator.ToString()});

                //TODO: code in  here to check if username unique 

                var targetUser = await db.Users.FirstOrDefaultAsync(x => x.Username == "Admin");
                if (targetUser == null)
                {
                    try
                    {
                        await userRepository.Register(user, "Password");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
        }
    }
}
