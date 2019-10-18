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
    public class LinksTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {

        [Fact]
        public async Task Can_remove_a_link_from_a_card()
        {
            //arrange 
            int expectedLinkCount = 1;
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

                    Links = new[]{
                        new LinkRequest(){Description = "test actionPoint", Link="http://www.testdomain.com/test"},
                        new LinkRequest(){Description = "test actionPoint", Link="http://www.test.co.uk/test"}
                    }
                }
            };
            var newResult = await Client.PostAsync(newRequest.Url, ContentHelper.GetStringContent(newRequest.Body));
            var newCard = JsonConvert.DeserializeObject<Card>(await newResult.Content.ReadAsStringAsync());
            var deleteRequest = new
            {
                Url = $"api/cards/{newCard.Id}",
                Body = new
                {

                    Title = testCardTitle,
                    Description = testCardDescription,

                    Links = new[]{
                        new LinkRequest(){Description = "test actionPoint", Link="http://www.testdomain.com/test"},
                    }
                }
            };

            //act
            var deleteResult =
                await Client.PutAsync(deleteRequest.Url, ContentHelper.GetStringContent(deleteRequest.Body));

            //assert
            var response = await Client.GetAsync(deleteRequest.Url);
            var card = JsonConvert.DeserializeObject<CardResponse>(await response.Content.ReadAsStringAsync());
            Assert.Equal(expectedLinkCount, card.Links.Count);
        }


        [Fact]
        public async Task Can_add_a_link_to_a_card()
        {
            //arrange
            int expectedLinkCount = 2;
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
                }
            };
            var newResult = await Client.PostAsync(newRequest.Url, ContentHelper.GetStringContent(newRequest.Body));
            var newCard = JsonConvert.DeserializeObject<Card>(await newResult.Content.ReadAsStringAsync());
            var updateRequest = new
            {
                Url = $"api/cards/{newCard.Id}",
                Body = new
                {
                    Title = testCardTitle,
                    Description = testCardDescription,
                    Links = new[]{
                        new LinkRequest(){Description = "test actionPoint", Link="http://www.testdomain.com/test"},
                        new LinkRequest(){Description = "test actionPoint", Link="http://www.test.co.uk/test"}
                    }
                }
            };
            //act
            var updateResult =
                await Client.PutAsync(updateRequest.Url, ContentHelper.GetStringContent(updateRequest.Body));

            //assert
            var response = await Client.GetAsync(updateRequest.Url);
            var card = JsonConvert.DeserializeObject<CardResponse>(await response.Content.ReadAsStringAsync());
            Assert.Equal(expectedLinkCount, card.Links.Count);
        }
        
        [Fact]
        public async Task Can_add_a_card_with_a_link()
        {
            //Arrange
            int expectedLinkCount = 2;
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

                    Links = new[]{
                        new LinkRequest(){Description = "test actionPoint", Link="http://www.testdomain.com/test"},
                        new LinkRequest(){Description = "test actionPoint", Link="http://www.test.co.uk/test"}
                    }
                }
            };

            
            //Act
            var newResult = await Client.PostAsync(newRequest.Url, ContentHelper.GetStringContent(newRequest.Body));
            var newCard = JsonConvert.DeserializeObject<Card>(await newResult.Content.ReadAsStringAsync());
            var request = new
            {
                Url = $"api/cards/{newCard.Id}"
            };
            //Assert
            var response = await Client.GetAsync(request.Url);
            var card = JsonConvert.DeserializeObject<CardResponse>(await response.Content.ReadAsStringAsync());
            Assert.Equal(expectedLinkCount, card.Links.Count);
        }
    }
}