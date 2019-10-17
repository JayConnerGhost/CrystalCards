using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CrystalCards.api;
using CrystalCards.Api.Dtos;
using CrystalCards.Api.Tests.Utils;
using CrystalCards.Models;
using Newtonsoft.Json;
using Xunit;

namespace CrystalCards.Api.Tests
{
    public class NPPointsTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {

        [Fact]
        public async Task Set_order_on_point()
        {
            //arrange
            int point1ExpectedOrder=1;
            int point2ExpectedOrder=3;
            string testCardTitle = "Edited Title";
            string testCardDescription = "Edited Description";
            var Client = Utilities<Startup>.CreateClient();
            var token = await Utilities<Startup>.RegisterandLoginUser("ghost", "test", Client);
            //Attach bearer token 
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Utilities<Startup>.StripTokenValue(token));
            var request = new
            {
                Url = $"api/cards",
                Body = new
                {

                    Title = testCardTitle,
                    Description = testCardDescription,
                    Order=1,
                    NPPoints = new[]
                    {
                        new NPPointRequest() {Direction = "Negative", Description = "test negative",Order=1},
                        new NPPointRequest() {Direction = "Negative", Description = "test negative2", Order=3}
                    }
                }
            };
            //act
            var result = await Client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            var newCard = JsonConvert.DeserializeObject<CardResponse>(await result.Content.ReadAsStringAsync());

            //assert
            var response = await Client.GetAsync(request.Url + "/" + newCard.Id);
            var card = JsonConvert.DeserializeObject<CardResponse>(await response.Content.ReadAsStringAsync());


            Assert.Equal(point1ExpectedOrder,card.NPPoints[0].Order);
            Assert.Equal(point2ExpectedOrder,card.NPPoints[1].Order);
        }


        [Fact]
        public async Task Create_a_card_with_negatives()
        {
            //arrange 
            int expectedNegativeCount = 2;
            string testCardTitle = "Edited Title";
            string testCardDescription = "Edited Description";
            var Client = Utilities<Startup>.CreateClient();
            var token = await Utilities<Startup>.RegisterandLoginUser("ghost", "test", Client);
            //Attach bearer token 
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Utilities<Startup>.StripTokenValue(token));
            var request = new
            {
                Url = $"api/cards",
                Body = new
                {

                    Title = testCardTitle,
                    Description = testCardDescription,

                    NPPoints = new[]{
                        new NPPointRequest(){Direction="Negative", Description = "test negative"},
                        new NPPointRequest(){Direction="Negative", Description = "test negative2"}
                    }
                }
            };

            //act
            var result = await Client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            var newCard = JsonConvert.DeserializeObject<CardResponse>(await result.Content.ReadAsStringAsync());

            //Assert
            var response = await Client.GetAsync(request.Url + "/" + newCard.Id);
            var card = JsonConvert.DeserializeObject<CardResponse>(await response.Content.ReadAsStringAsync());

            Assert.Equal(expectedNegativeCount, card.NPPoints.Where(x=>x.Direction=="Negative").ToList().Count);
        }

        [Fact]
        public async Task Add_negative_to_card()
        {
            int expectedNegativeCount = 2;
            string testCardTitle = "Edited Title";
            string testCardDescription = "Edited Description";
            var Client = Utilities<Startup>.CreateClient();
            var token = await Utilities<Startup>.RegisterandLoginUser("ghost", "test", Client);
            //Attach bearer token 
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Utilities<Startup>.StripTokenValue(token));
            var id = await Utilities<Startup>.SetupACardReturnId("test", "test", Client);
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
            var card = JsonConvert.DeserializeObject<CardResponse>(await response.Content.ReadAsStringAsync());
            Assert.Equal(expectedNegativeCount, card.NPPoints.Where(x=>x.Direction=="Negative").ToList().Count);

        }
        [Fact]
        public async Task Delete_negative_from_card()
        {
            //arrange 
            int expectedNegativeCount = 1;
            string testCardTitle = "Edited Title";
            string testCardDescription = "Edited Description";
            var Client = Utilities<Startup>.CreateClient();
            var token = await Utilities<Startup>.RegisterandLoginUser("ghost", "test", Client);
            //Attach bearer token 
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Utilities<Startup>.StripTokenValue(token));
            var newRequest = new
            {
                Url = $"api/cards",
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
                        new NPPointRequest(){Id= 1,Direction = "Negative", Description = "test negative"},
                    }
                }
            };
            //act
            var updateResult = await Client.PutAsync(request.Url, ContentHelper.GetStringContent(updateRequest.Body));

            //assert
            var response = await Client.GetAsync(request.Url);
            var card = JsonConvert.DeserializeObject<CardResponse>(await response.Content.ReadAsStringAsync());
            Assert.Equal(expectedNegativeCount, card.NPPoints.Where(x=>x.Direction=="Negative").ToList().Count);
        }

        [Fact]
        public async Task Delete_positive_from_card()
        {
            //arrange 
            int expectedPositiveCount = 1;
            string testCardTitle = "Edited Title";
            string testCardDescription = "Edited Description";
            var Client = Utilities<Startup>.CreateClient();
            var token = await Utilities<Startup>.RegisterandLoginUser("ghost", "test", Client);
            //Attach bearer token 
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Utilities<Startup>.StripTokenValue(token));
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
            var card = JsonConvert.DeserializeObject<CardResponse>(await response.Content.ReadAsStringAsync());
            Assert.Equal(expectedPositiveCount, card.NPPoints.Where(x=>x.Direction=="Positive").ToList().Count);
        }


        [Fact]
        public async Task Add_positive_to_card()
        {
            //arrange 
            int expectedPositiveCount = 2;
            string testCardTitle = "Edited Title";
            string testCardDescription = "Edited Description";
            var Client = Utilities<Startup>.CreateClient();
            var token = await Utilities<Startup>.RegisterandLoginUser("ghost", "test", Client);
            //Attach bearer token 
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Utilities<Startup>.StripTokenValue(token));
            var id = await Utilities<Startup>.SetupACardReturnId("test", "test", Client);
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
            var card = JsonConvert.DeserializeObject<CardResponse>(await response.Content.ReadAsStringAsync());
            Assert.Equal(expectedPositiveCount, card.NPPoints.Where(x => x.Direction == "Positive").ToList().Count);
        }


        [Fact]
        public async Task Create_a_card_with_positives()
        {
            //arrange 
            int expectedPositiveCount = 2;
            string testCardTitle = "Edited Title";
            string testCardDescription = "Edited Description";
            var Client = Utilities<Startup>.CreateClient();
            var token = await Utilities<Startup>.RegisterandLoginUser("ghost", "test", Client);
            //Attach bearer token 
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Utilities<Startup>.StripTokenValue(token));
            var request = new
            {
                Url = "api/cards",
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
            var newCard = JsonConvert.DeserializeObject<CardResponse>(await result.Content.ReadAsStringAsync());

            //Assert
            var response = await Client.GetAsync(request.Url + "/" + newCard.Id);
            var card = JsonConvert.DeserializeObject<CardResponse>(await response.Content.ReadAsStringAsync());

            Assert.Equal(expectedPositiveCount, card.NPPoints.Where(x => x.Direction == "Positive").ToList().Count);
        }

    }
}