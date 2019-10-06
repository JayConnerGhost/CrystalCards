using System.Linq;
using System.Threading.Tasks;
using CrystalCards.api;
using CrystalCards.Api.Dtos;
using Newtonsoft.Json;
using Xunit;

namespace CrystalCards.Api.Tests
{
    public class ActionPointTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private CustomWebApplicationFactory<Startup> _factory = new CustomWebApplicationFactory<Startup>();

        [Fact]
        public async Task Can_create_a_card_with_a_actionPoint()
        {
            //arrange 
            int expectedActionPointCount = 2;
            string testCardTitle = "Edited Title";
            string testCardDescription = "Edited Description";
            var Client = _factory.CreateClient();
            var request = new
            {
                Url = $"api/cards",
                Body = new
                {

                    Title = testCardTitle,
                    Description = testCardDescription,

                    ActionPoints = new[]{
                        new ActionPointRequest(){Description = "test actionPoint"},
                        new ActionPointRequest(){Description = "test actionPoint2"}
                    }
                }
            };

            //act
            var result = await Client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            var newCard = JsonConvert.DeserializeObject<CardResponse>(await result.Content.ReadAsStringAsync());

            //assert

            var response = await Client.GetAsync(request.Url + "/" + newCard.Id);
            var card = JsonConvert.DeserializeObject<CardResponse>(await response.Content.ReadAsStringAsync());

            Assert.Equal(expectedActionPointCount, card.ActionPoints.Count);

        }

    }
}