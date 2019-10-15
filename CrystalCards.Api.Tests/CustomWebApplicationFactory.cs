using System;
using System.IO;
using System.Net.Http;
using CrystalCards.api;
using CrystalCards.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace CrystalCards.Api.Tests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Create a new service provider.
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                // Add a database context (ApplicationDbContext) using an in-memory 
                // database for testing.
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                    options.UseInternalServiceProvider(serviceProvider);
                });

                // Build the service provider.
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database
                // context (ApplicationDbContext).
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<ApplicationDbContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    // Ensure the database is created.
                    db.Database.EnsureCreated();

                    try
                    {
                        // Seed the database with test data.
                        //Utilities.InitializeDbForTests(db);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                                            $"database with test messages. Error: {ex.Message}");
                    }
                }
            });
        }

        public class HttpTestBase : IDisposable, IClassFixture<CustomWebApplicationFactory<Startup>>
        {
            private IWebHostBuilder _builder;
            public TestServer TestServer;
            public HttpClient Client;

            public readonly WebApplicationFactory<Startup> _factory;

            public HttpTestBase()
            {
                BuildHttpClient();

            }

            private void BuildHttpClient()
            {
                //https://geeklearning.io/how-to-deal-with-identity-when-testing-an-asp-net-core-application/
                //TODO: refactor this to allow setting the environment to test 
                Client = _factory.CreateClient();
                //Create a webapplication host factory and custom start up code include inmemory database. 

                //            _builder = new WebHostBuilder()
                //                .UseContentRoot(GetPathToWebApplication())
                //                .UseEnvironment("Development")
                //                .UseStartup<CrystalCards.api.Startup>();


//                TestServer = new TestServer(_builder);
//            Client = TestServer.CreateClient();
            }

            private string GetPathToWebApplication()
            {
                string appRootPath = Path.GetFullPath(Path.Combine(
                    AppContext.BaseDirectory,
                    "..", "..", "..", "..", "CrystalCards.Api"));
                //add code to get to executing web application you wish to host 
                var pathToWebApplication = appRootPath;
                return pathToWebApplication;
            }


            public void Dispose()
            {
                _builder = null;
                TestServer.Dispose();
                Client.Dispose();
            }
        }
    }
}