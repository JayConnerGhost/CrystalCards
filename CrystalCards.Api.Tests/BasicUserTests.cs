using System.Collections.Generic;
using System.Net;
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
    public class BasicUserTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private CustomWebApplicationFactory<Startup> _factory = new CustomWebApplicationFactory<Startup>();

        [Fact]
        public async Task User_can_not_access_user_controller()
        {
            var Client = Utilities<Startup>.CreateClient();
            var user = await Utilities<Startup>.RegisterandLoginUser("ghost", "test", Client);
            //Attach bearer token 
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Utilities<Startup>.StripTokenValue(user));

            var requestGetUsers = new
            {
                Url = $"api/users"
            };


            //act
            var response=await Client.GetAsync(requestGetUsers.Url);
            
            //assert
            Assert.Equal(HttpStatusCode.Forbidden,response.StatusCode);
        }

        [Fact]
        public async Task Administrator_can_access_users()
        {
            var Client = Utilities<Startup>.CreateClient();
            var user = await Utilities<Startup>.LoginUser("test", "ghostAdmin", Client);
            //Attach bearer token 
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Utilities<Startup>.StripTokenValue(user));

            var requestGetUsers = new
            {
                Url = $"api/users"
            };


            //act
            var response=await Client.GetAsync(requestGetUsers.Url);
            
            //assert
            Assert.Equal(HttpStatusCode.OK,response.StatusCode);
            
        }

        [Fact]
        public async Task Retreive_list_of_users_as_userResponses()
        {
            //arrange
            int expectedUserCount=8;
            //need to use the built in test admin user for this 
            var Client = Utilities<Startup>.CreateClient();
            var user = await Utilities<Startup>.LoginUser("test", "ghostAdmin", Client);
            //Attach bearer token 
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Utilities<Startup>.StripTokenValue(user));

            var requestGetUsers = new
            {
                Url = $"api/users"
            };
            await Utilities<Startup>.RegisterUser("Test", "testUser1", Client);
            await Utilities<Startup>.RegisterUser("Test", "testUser2", Client);
            await Utilities<Startup>.RegisterUser("Test", "testUser3", Client);
            await Utilities<Startup>.RegisterUser("Test", "testUser4", Client);
            await Utilities<Startup>.RegisterUser("Test", "testUser5", Client);
            await Utilities<Startup>.RegisterUser("Test", "testUser6", Client);
            await Utilities<Startup>.RegisterUser("Test", "testUser7", Client);


            //act
            var response = await Client.GetAsync(requestGetUsers.Url);
            var users = JsonConvert.DeserializeObject<List<UserResponse>>(await response.Content.ReadAsStringAsync());

            //Assert
            Assert.Equal(expectedUserCount,users.Count);
        }

        [Fact]
        public async Task Delete_user()
        {
            //arrange 
           
            var Client = Utilities<Startup>.CreateClient();
            var user = await Utilities<Startup>.LoginUser("test", "ghostAdmin", Client);
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Utilities<Startup>.StripTokenValue(user));
            await Utilities<Startup>.RegisterUser("Test", "testUser11", Client);

            var requestGetUsers = new
            {
                Url = $"api/users/testUser11"
            };

            //act

            //TODO issue delete statement

            //assert
            var response = await Client.GetAsync(requestGetUsers.Url);
            var userResponse = JsonConvert.DeserializeObject<UserResponse>(await response.Content.ReadAsStringAsync());

            Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);
            Assert.Equal(null,userResponse);
        }

        [Fact]
        public async Task Can_retrieve_user_by_userName()
        {
            //arrange
            var userName = "testUser11";
            var Client = Utilities<Startup>.CreateClient();
            var user = await Utilities<Startup>.LoginUser("test", "ghostAdmin", Client);
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Utilities<Startup>.StripTokenValue(user));
           
            await Utilities<Startup>.RegisterUser("Test", userName, Client);
            var requestGetUser = new
            {
                Url = $"api/users/{userName}"
            };

            //act
            var response = await Client.GetAsync(requestGetUser.Url);

            //assert
          
            var userResponse = JsonConvert.DeserializeObject<UserResponse>(await response.Content.ReadAsStringAsync());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(userResponse);
        }

        [Fact]
        public async Task User_has_a_card()
        {
            //arrange 
            int expectedCardCount=1;
            string cardTitle = "Edited Title";
            string cardDescription = "Edited Description";
            var Client = Utilities<Startup>.CreateClient();
            var user = await Utilities<Startup>.RegisterandLoginUser("ghost", "test", Client);
            var username = Utilities<Startup>.StripUserNameValue(user);

            //Attach bearer token 
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Utilities<Startup>.StripTokenValue(user));
            //create a Card 
            var requestCreateCard = new
            {
                Url=$"api/cards/{username}",
                Body =new
                {
                    Title = cardTitle,
                    Description = cardDescription
                }
            };
            var request = new 
            {
                Url=$"api/cards/GetForUserName/{username}"
            };

            //act
            var updateResult = await Client.PostAsync(requestCreateCard.Url, ContentHelper.GetStringContent(requestCreateCard.Body));

            //assert
            var response = await Client.GetAsync(request.Url);
            var cards = JsonConvert.DeserializeObject<List<Card>>(await response.Content.ReadAsStringAsync());

            Assert.Equal(expectedCardCount, cards.Count);
        }



    }
}