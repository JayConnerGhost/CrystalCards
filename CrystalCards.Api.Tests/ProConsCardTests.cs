using System.Threading.Tasks;
using CrystalCards.api;
using CrystalCards.Models;
using Newtonsoft.Json;
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
            int expectedCount=1;
            var Client = _factory.CreateClient();
            var id = await Utilities.SetupACardReturnId("test", "test");
            var cardRequest = new
            {
                Url = $"api/cards/{id}",
            };
            //Act
            var NPPointRequest=new 
            {
                Url=$"api/nppoint",
                Body =new {
                    CardId=$"{id}",
                    Direction=$"{NPPointDirection.Positive}"
                }
            };
            await Client.PostAsync(NPPointRequest.Url, ContentHelper.GetStringContent(NPPointRequest.Body));


            //Assert
            var updatedCardResponse = await Client.GetAsync(cardRequest.Url);
            var card = JsonConvert.DeserializeObject<Card>(await updatedCardResponse.Content.ReadAsStringAsync());
            Assert.Equal(card.Positives.Count,expectedCount);
        }
    }
}