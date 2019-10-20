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
            var authorizationHeader = await Utilities<Startup>.RegisterandLoginUser("ghost", "test", Client);
            var userName = Utilities<Startup>.StripUserNameValue(authorizationHeader);

            //Attach bearer token 
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Utilities<Startup>.StripTokenValue(authorizationHeader));
            var request = new
            {
                Url = $"api/cards/{userName}",
                Body = new
                {
                    Title = testCardTitle,
                    Description = testCardDescription,
                        NPPoints = new[]{
                         new NPPointRequest(){Direction="Negative", Description = "test negative2"}
                         }
                }
            };
            var getRequest = new
            {
                Url = $"api/cards/GetForUserName/{userName}"
            };

            //act
            var newResponse = await Client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            var response = await Client.GetAsync(getRequest.Url);
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
            var authorizationHeader = await Utilities<Startup>.RegisterandLoginUser("ghost", "test", Client);
            //Attach bearer token 
            var userName = Utilities<Startup>.StripUserNameValue(authorizationHeader);

            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Utilities<Startup>.StripTokenValue(authorizationHeader));
            var request = new
            {
                Url = $"api/cards/{userName}",
                Body = new
                {
                    Title = testCardTitle,
                    Description = testCardDescription,
                    NPPoints = new[]{
                        new NPPointRequest(){Direction="Positive", Description = "test positive"}
                    }
                }
            };
            var getRequest = new
            {
                Url=$"/api/cards/GetForUserName/{userName}"
            };

            //act
            var newResponse = await Client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            var response = await Client.GetAsync(getRequest.Url);
            //assert
            var cardResponses = JsonConvert.DeserializeObject<List<CardResponse>>(await response.Content.ReadAsStringAsync());
            var testValue = cardResponses[0].NPPoints[0].Direction;
            Assert.Equal(expectedValue, testValue);
        }
    }
}