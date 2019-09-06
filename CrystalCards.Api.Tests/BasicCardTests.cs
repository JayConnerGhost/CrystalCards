using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CrystalCards.api;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;


namespace CrystalCards.Api.Tests
{
   
        public static class ContentHelper
        {
            public static StringContent GetStringContent(object obj)
                => new StringContent(JsonConvert.SerializeObject(obj), Encoding.Default, "application/json");
        }
    
    public class BasicCardTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private CustomWebApplicationFactory<Startup> _factory = new CustomWebApplicationFactory<Startup>();
        public BasicCardTests() : base()
        {
        }

        [Fact]
        public async Task Receive_a_created_status_code_adding_card()
        {
            //Arrange
            //Set up a post 
            var request = new
            {
                Url = "api/cards",
                Body = new
                {
                   Title="Test Card 1",
                   Description="Test Card 1 description"
                }
            };
            var Client = _factory.CreateClient();
            //Set up the base class to host the http in memory
            var response = await Client.PostAsync(request.Url,ContentHelper.GetStringContent(request.Body));
            //Act
            //HttpStatusCode receivedStatusCode;

            var statusCode =  response.StatusCode;
            //Assert

            Assert.Equal(HttpStatusCode.Created,statusCode);
        }
    }
}
