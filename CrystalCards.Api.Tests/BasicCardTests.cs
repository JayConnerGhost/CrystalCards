using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CrystalCards.api;
using CrystalCards.Api.Tests.Utils;
using CrystalCards.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;


namespace CrystalCards.Api.Tests
{
    public class BasicCardTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {

        private CustomWebApplicationFactory<Startup> _factory = new CustomWebApplicationFactory<Startup>();
        public BasicCardTests() : base()
        {
        }


        [Fact]
        public async Task Delete_a_card_by_id()
        {
            //arrange
            var Client = Utilities<Startup>.CreateClient();
            var token = await Utilities<Startup>.RegisterandLoginUser("ghost", "test", Client);
            //Attach bearer token 
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Utilities<Startup>.StripTokenValue(token));
            var userName = Utilities<Startup>.StripUserNameValue(token);

            var id = await Utilities<Startup>.SetupACardReturnId("test", "test", Client, userName);

            var request = new
            {
                Url = $"api/cards/{id}",
            };
            //act
            var deleteResult = await Client.DeleteAsync(request.Url);
            Assert.Equal(HttpStatusCode.NoContent, deleteResult.StatusCode);
            //assert
            var response = await Client.GetAsync(request.Url);
            var card = JsonConvert.DeserializeObject<Card>(await response.Content.ReadAsStringAsync());
           

            Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);
        }

        [Fact]
        public async Task Update_a_card()
        {
            //arrange 
            string testEditedCardTitle="Edited Title";
            string testEditedCardDescription="Edited Description";
         
            var Client = Utilities<Startup>.CreateClient();
            var token =await Utilities<Startup>.RegisterandLoginUser("ghost", "test", Client);
            //Attach bearer token 
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Utilities<Startup>.StripTokenValue(token));
            var userName = Utilities<Startup>.StripUserNameValue(token);

            var id = await Utilities<Startup>.SetupACardReturnId("test", "test",Client, userName);
            var request = new
            {
                Url = $"api/cards/{id}",
            };
            var updateRequest = new
            {
                Body = new
                {
                  
                    Title = testEditedCardTitle,
                    Description = testEditedCardDescription
                }
            };
            //act

            //Perform card update 
            var updateResult=await Client.PutAsync(request.Url, ContentHelper.GetStringContent(updateRequest.Body));


            //assert
            var response = await Client.GetAsync(request.Url);
            var card = JsonConvert.DeserializeObject<Card>(await response.Content.ReadAsStringAsync());
            Assert.Equal(card.Title, testEditedCardTitle);
            Assert.Equal(card.Description, testEditedCardDescription);
        }


        [Fact]
        public async Task Retrieve_a_card()
        {
            //Arrange
            var testCardTitle = "Test Card 2";
            var testCardDescription = "Test Card 2 description";
            var Client = Utilities<Startup>.CreateClient();
            var token = await Utilities<Startup>.RegisterandLoginUser("ghost", "test", Client);
            //Attach bearer token 
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Utilities<Startup>.StripTokenValue(token));

            var userName = Utilities<Startup>.StripUserNameValue(token);

            var request = new
            {
                Url = $"api/cards/{userName}",
                Body = new
                {
                    Title = testCardTitle,
                    Description = testCardDescription
                }
            };
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
    
            var Client = Utilities<Startup>.CreateClient();
            var token = await Utilities<Startup>.RegisterandLoginUser("ghost", "test", Client);
            //Attach bearer token 
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Utilities<Startup>.StripTokenValue(token));
            var userName = Utilities<Startup>.StripUserNameValue(token);


            var request = new
            {
                Url = $"api/cards/{userName}",
                Body = new
                {
                    Title = "Test Card 1",
                    Description = "Test Card 1 description"
                }
            };

            //Act
            var response = await Client.PostAsync(request.Url,ContentHelper.GetStringContent(request.Body));

            var statusCode =  response.StatusCode;

            //Assert

            Assert.Equal(HttpStatusCode.Created,statusCode);
        }

        [Fact]
        public async Task Get_Multiple_cards()
        {

            var Client = Utilities<Startup>.CreateClient();
            var authorizationResponse = await Utilities<Startup>.RegisterandLoginUser("ghost", "test", Client);
            var userName = Utilities<Startup>.StripUserNameValue(authorizationResponse);

            //Arrange 
            int expectedCount =3;
            //set up 3 cards 
            var request = new
            {
                Url = $"api/cards/{userName}",
                Body = new
                {
                    Title = "Test Card 1",
                    Description = "Test Card 1 description"
                }
            };
            var request2 = new
            {
                Url = $"api/cards/{userName}",
                Body = new
                {
                    Title = "Test Card 2",
                    Description = "Test Card 2 description"
                }
            };
            var request3 = new
            {
                Url = $"api/cards/{userName}",
                Body = new
                {
                    Title = "Test Card 3",
                    Description = "Test Card 3 description"
                }
            };

            var getRequest = new
            {
                Url = $"api/cards/GetForUserName/{userName}",
            };
  
            //Attach bearer token 
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Utilities<Startup>.StripTokenValue(authorizationResponse));
            //Act

            await Client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
             await Client.PostAsync(request2.Url, ContentHelper.GetStringContent(request2.Body));
             await Client.PostAsync(request3.Url, ContentHelper.GetStringContent(request3.Body));
            
            //Assert
            var response= await Client.GetAsync(getRequest.Url);
            List<Card> returnedCardCollection = JsonConvert.DeserializeObject<List<Card>>(await response.Content.ReadAsStringAsync());
            Assert.Equal(returnedCardCollection.Count,expectedCount);
        }

    
    }
}
