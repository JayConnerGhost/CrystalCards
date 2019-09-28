using System.Threading.Tasks;
using CrystalCards.api;
using CrystalCards.Api.Dtos;
using CrystalCards.Models;
using Newtonsoft.Json;
using Xunit;

namespace CrystalCards.Api.Tests
{
    public class NPPointsTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private CustomWebApplicationFactory<Startup> _factory = new CustomWebApplicationFactory<Startup>();

        [Fact]
        public async Task Add_negative_to_card()
        {
            int expectedNegativeCount = 2;
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

                    NPPoints = new[]{
                        new NPPointRequest(){Direction="Negative", Description = "test negative"},
                        new NPPointRequest(){Direction="Negative", Description = "test negative"}
                    }
                }
            };
            //act
            var updateResult = await Client.PutAsync(request.Url, ContentHelper.GetStringContent(updateRequest.Body));

            //Assert
            var response = await Client.GetAsync(request.Url);
            var card = JsonConvert.DeserializeObject<Card>(await response.Content.ReadAsStringAsync());
            Assert.Equal(expectedNegativeCount, card.Negatives.Count);

        }


        [Fact]
        public async Task Delete_positive_from_card()
        {
            //arrange 
            int expectedPositiveCount = 1;
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

                    NPPoints = new[]{
                        new NPPointRequest(){Direction="Positive", Description = "test positive"},
                        new NPPointRequest(){Direction="Positive", Description = "test positive2"}
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

                    NPPoints = new[]{
                        new NPPointRequest(){Id= 1,Direction = "Positive", Description = "test positive"},
                    }
                }
            };
            //act
            var updateResult = await Client.PutAsync(request.Url, ContentHelper.GetStringContent(updateRequest.Body));

            //assert
            var response = await Client.GetAsync(request.Url);
            var card = JsonConvert.DeserializeObject<Card>(await response.Content.ReadAsStringAsync());
            Assert.Equal(expectedPositiveCount, card.Positives.Count);
        }


        [Fact]
        public async Task Add_positive_to_card()
        {
            //arrange 
            int expectedPositiveCount = 2;
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

                    NPPoints = new[]{
                        new NPPointRequest(){Direction="Positive", Description = "test positive"},
                        new NPPointRequest(){Direction="Positive", Description = "test positive2"}
                    }
                }
            };
            //act
            var updateResult = await Client.PutAsync(request.Url, ContentHelper.GetStringContent(updateRequest.Body));

            //Assert
            var response = await Client.GetAsync(request.Url);
            var card = JsonConvert.DeserializeObject<Card>(await response.Content.ReadAsStringAsync());
            Assert.Equal(expectedPositiveCount, card.Positives.Count);
        }


        [Fact]
        public async Task Create_a_card_with_positives()
        {
            //arrange 
            int expectedPositiveCount = 2;
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

                    NPPoints = new[]{
                        new NPPointRequest(){Direction="Positive", Description = "test positive"},
                        new NPPointRequest(){Direction="Positive", Description = "test positive2"}
                    }
                }
            };

            //act
            var result = await Client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            var newCard = JsonConvert.DeserializeObject<Card>(await result.Content.ReadAsStringAsync());

            //Assert
            var response = await Client.GetAsync(request.Url + "/" + newCard.Id);
            var card = JsonConvert.DeserializeObject<Card>(await response.Content.ReadAsStringAsync());

            Assert.Equal(expectedPositiveCount, card.Positives.Count);
        }

    }
}