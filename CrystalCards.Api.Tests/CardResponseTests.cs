using System.Collections.Generic;
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
    public class CardResponseTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private CustomWebApplicationFactory<Startup> _factory =new CustomWebApplicationFactory<Startup>();

        [Fact]
        public async Task Response_uses_the_text_negative_for_negative_point_direction()
        {
            //arrange 
            string expectedValue="Negative";
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
                         new NPPointRequest(){Direction="Negative", Description = "test negative2"}
                         }
                }
            };

            //act
            var newResponse = await Client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            var response = await Client.GetAsync(request.Url);
            //assert
            var cardResponses = JsonConvert.DeserializeObject<List<CardResponse>>(await response.Content.ReadAsStringAsync());
            var testValue = cardResponses[0].NPPoints[0].Direction;
            Assert.Equal(expectedValue,testValue);
        }
        [Fact]
        public async Task Response_uses_the_text_positive_for_positive_point_direction()
        {
            //arrange 
            string expectedValue = "Positive";
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
                        new NPPointRequest(){Direction="Positive", Description = "test positive"}
                    }
                }
            };

            //act
            var newResponse = await Client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            var response = await Client.GetAsync(request.Url);
            //assert
            var cardResponses = JsonConvert.DeserializeObject<List<CardResponse>>(await response.Content.ReadAsStringAsync());
            var testValue = cardResponses[0].NPPoints[0].Direction;
            Assert.Equal(expectedValue, testValue);
        }
    }
}