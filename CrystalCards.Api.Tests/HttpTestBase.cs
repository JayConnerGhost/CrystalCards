using System;
using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace CrystalCards.Api.Tests
{
    public class HttpTestBase : IDisposable
    {
        private IWebHostBuilder _builder;
        public TestServer TestServer;
        public HttpClient Client;
        public HttpTestBase()
        {
            BuildHttpClient();
        }

        private void BuildHttpClient()
        {

            _builder = new WebHostBuilder()
                .UseContentRoot(GetPathToWebApplication())
                .UseEnvironment("Development")
                .UseStartup<CrystalCards.api.Startup>();


            TestServer = new TestServer(_builder);
            Client = TestServer.CreateClient();
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