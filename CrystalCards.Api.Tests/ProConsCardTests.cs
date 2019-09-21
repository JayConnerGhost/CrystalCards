using System.Threading.Tasks;
using CrystalCards.api;
using CrystalCards.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using Xunit;

namespace CrystalCards.Api.Tests
{
    public class ProConsCardTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private CustomWebApplicationFactory<Startup> _factory = new CustomWebApplicationFactory<Startup>();

        [Fact]
        public async Task Can_add_a_Pro_to_a_card()
        {
            //Arrange 
            var expectedCount=1;
            var id = await Utilities<Startup>.SetupACardReturnId("test", "test", _factory);
            var Client = _factory.CreateClient();
            var request = new
            {
                Url = $"api/cards/{id}",
            };
            var updateRequest = new
            {
                Url = $"api/cards/{id}",
                Body=new
                {
                    Id =id,
                    NPPoints=new[]
                    {
                      new{ Direction="Positive"}
                    }
                }
            };
            var compiledRequest = ContentHelper.GetStringContent(updateRequest.Body);
            //act
            var updateResponse = await Client.PutAsync(updateRequest.Url, compiledRequest);
            

            //Assert
            var response = await Client.GetAsync(request.Url);
            var card = JsonConvert.DeserializeObject<Card>(await response.Content.ReadAsStringAsync());

            Assert.Equal(expectedCount,card.Positives.Count);
        }
    }
}