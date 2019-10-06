using System.Linq;
using System.Threading.Tasks;
using CrystalCards.api;
using CrystalCards.Api.Dtos;
using CrystalCards.Models;
using Newtonsoft.Json;
using Xunit;

namespace CrystalCards.Api.Tests
{
    public class ActionPointTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private CustomWebApplicationFactory<Startup> _factory = new CustomWebApplicationFactory<Startup>();

        [Fact]
        public async Task Can_delete_a_actionPoint_from_a_card()
        {
            //arrange
            int expectedActionPointCount = 1;
            string testCardTitle = "Edited Title";
            string testCardDescription = "Edited Description";
            var Client = _factory.CreateClient();
            var newRequest = new
            {
                Url = $"api/cards",
                Body = new
                {

                    Title = testCardTitle,
                    Description = testCardDescription,

                    ActionPoints = new[]{
                        new NPPointRequest(){Description = "test actionPoint"},
                        new NPPointRequest(){Description = "test actionPoint"}
                    }
                }
            };

            var newResult = await Client.PostAsync(newRequest.Url, ContentHelper.GetStringContent(newRequest.Body));
            var newCard = JsonConvert.DeserializeObject<Card>(await newResult.Content.ReadAsStringAsync());

            var request = new
            {
                Url = $"api/cards/{newCard.Id}",
            };
            var updateRequest = new
            {
                Body = new
                {

                    Title = testCardTitle,
                    Description = testCardDescription,

                    ActionPoints = new[]{
                        new NPPointRequest(){Id= 1, Description = "test actionPoint"},
                    }
                }
            };
            //act
            var updateResult = await Client.PutAsync(request.Url, ContentHelper.GetStringContent(updateRequest.Body));

            //assert
            var response = await Client.GetAsync(request.Url);
            var card = JsonConvert.DeserializeObject<CardResponse>(await response.Content.ReadAsStringAsync());
            Assert.Equal(expectedActionPointCount, card.ActionPoints.Count);

        }

        [Fact]
        public async Task Can_add_a_actionPoint_to_a_card()
        {
            //arrange
            int expectedActionPointCount = 2;
            string testCardTitle = "Edited Title";
            string testCardDescription = "Edited Description";
            var Client = _factory.CreateClient();
            var id = await Utilities<Startup>.SetupACardReturnId("test", "test", _factory);

            var request = new
            {
                Url = $"api/cards/{id}",
            };
            var updateRequest = new
            {
                Body = new
                {

                    Title = testCardTitle,
                    Description = testCardDescription,

                    ActionPoints = new[]
                    {
                        new ActionPointRequest() {Description = "test actionPoint"},
                        new ActionPointRequest() {Description = "test actionPoint2"}
                    }
                }
            };
            //act
            var updateResult = await Client.PutAsync(request.Url, ContentHelper.GetStringContent(updateRequest.Body));
            
            //Assert
            var response = await Client.GetAsync(request.Url);
            var card = JsonConvert.DeserializeObject<CardResponse>(await response.Content.ReadAsStringAsync());
            Assert.Equal(expectedActionPointCount, card.ActionPoints.Count);

        }


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

                    ActionPoints = new[]
                    {
                        new ActionPointRequest() {Description = "test actionPoint"},
                        new ActionPointRequest() {Description = "test actionPoint2"}
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