using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CrystalCards.api;
using CrystalCards.Models;
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
        public async Task Retrieve_a_card()
        {
            //Arrange
            var testCardTitle = "Test Card 2";
            var testCardDescription = "Test Card 2 description";
            var request = new
            {
                Url = "api/cards",
                Body = new
                {
                    Title = testCardTitle,
                    Description = testCardDescription
                }
            };
            var Client = _factory.CreateClient();

            //Act
            var response = await Client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));


            //Assert
            var card = JsonConvert.DeserializeObject<Card>(await response.Content.ReadAsStringAsync());
            Assert.Equal(card.Title,testCardTitle);
            Assert.Equal(card.Description,testCardDescription);
        }


        [Fact]
        public async Task Receive_a_created_status_code_adding_card()
        {
            //Arrange
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

            //Act
            var response = await Client.PostAsync(request.Url,ContentHelper.GetStringContent(request.Body));

            var statusCode =  response.StatusCode;

            //Assert

            Assert.Equal(HttpStatusCode.Created,statusCode);
        }

        [Fact]
        public async Task Get_Multiple_cards()
        {
            //Arrange 
            int expectedCount=3;
            //set up 3 cards 
            var request = new
            {
                Url = "api/cards",
                Body = new
                {
                    Title = "Test Card 1",
                    Description = "Test Card 1 description"
                }
            };
            var request2 = new
            {
                Url = "api/cards",
                Body = new
                {
                    Title = "Test Card 2",
                    Description = "Test Card 2 description"
                }
            };
            var request3 = new
            {
                Url = "api/cards",
                Body = new
                {
                    Title = "Test Card 3",
                    Description = "Test Card 3 description"
                }
            };

            var Client = _factory.CreateClient();
            //Act
            
             await Client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
             await Client.PostAsync(request2.Url, ContentHelper.GetStringContent(request2.Body));
             await Client.PostAsync(request3.Url, ContentHelper.GetStringContent(request3.Body));
            
            //Assert
            var response= await Client.GetAsync(request.Url);
            List<Card> returnedCardCollection = JsonConvert.DeserializeObject<List<Card>>(await response.Content.ReadAsStringAsync());
            Assert.Equal(returnedCardCollection.Count,expectedCount);
        }
    }
}
