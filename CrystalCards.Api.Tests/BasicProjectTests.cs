using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CrystalCards.api;
using CrystalCards.Api.Dtos;
using CrystalCards.Api.Tests.Utils;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Xunit;

namespace CrystalCards.Api.Tests
{
    public class BasicProjectTests: IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private CustomWebApplicationFactory<Startup> _factory = new CustomWebApplicationFactory<Startup>();

        public BasicProjectTests() : base()
        {
        }

        [Fact]
        public async Task Can_add_card_to_project()
        {
            // arrange
            string testProjectTitle = "Test project";
            string expectedCardTitle = "test card title";
            var Client = Utilities<Startup>.CreateClient();
            //set up user 
            var token = await Utilities<Startup>.RegisterandLoginUser("ghost", "test", Client);
            //Attach bearer token 
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Utilities<Startup>.StripTokenValue(token));
            var userName = Utilities<Startup>.StripUserNameValue(token);
            //set up project
            var addRequest = new
            {
                Url = $"api/projects/{userName}",
                Body = new
                {
                    id = 0,
                    Title = testProjectTitle,
                }
            };
            var addResult = await Client.PostAsync(addRequest.Url, ContentHelper.GetStringContent(addRequest.Body));
            var addProjectResponse = JsonConvert.DeserializeObject<ProjectResponse>(await addResult.Content.ReadAsStringAsync());
            var projectId = addProjectResponse.Id;
            //set up card 
            var cardId = await Utilities<Startup>.SetupACardReturnId("test", expectedCardTitle, Client, userName);

            //Act

            var addCardToProjectRequest = new
            {
                Url = $"api/projects/AddCardToProject/{projectId}",
                Body = new
                {
                    projectId = projectId,
                    cardId = cardId
                }
            };

            var project = await Client.PostAsync(addCardToProjectRequest.Url,
                ContentHelper.GetStringContent(addCardToProjectRequest.Body));
            //assert

            var getRequest = new
            {
                Url = $"api/projects/GetForProjectId/{projectId}"
            };
            var getResponse = await Client.GetAsync(getRequest.Url);
            var projectResponse = JsonConvert.DeserializeObject<ProjectResponse>(await getResponse.Content.ReadAsStringAsync());
            var card = projectResponse.Cards[0];
            Assert.Equal(expectedCardTitle, card.Title);
        }


        [Fact]
        public async Task Can_get_card_by_id()
        {
            //arrange
            var expectedProjectTitle="test project";
            var Client = Utilities<Startup>.CreateClient();
            //set up user 
            var token = await Utilities<Startup>.RegisterandLoginUser("ghost", "test", Client);
            //Attach bearer token 
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Utilities<Startup>.StripTokenValue(token));
            var userName = Utilities<Startup>.StripUserNameValue(token);
            var addRequest = new
            {
                Url = $"api/projects/{userName}",
                Body = new
                {
                    id = 0,
                    Title = expectedProjectTitle,
                }
            };
            var addResult = await Client.PostAsync(addRequest.Url, ContentHelper.GetStringContent(addRequest.Body));
            var addProjectResponse = JsonConvert.DeserializeObject<ProjectResponse>(await addResult.Content.ReadAsStringAsync());
            //act

            var getRequest = new
            {
                Url = $"api/projects/GetForProjectId/{addProjectResponse.Id}"
            };
            var projectResponse = await Client.GetAsync(getRequest.Url);

            //assert
            var project = JsonConvert.DeserializeObject<ProjectResponse>(await projectResponse.Content.ReadAsStringAsync());
            Assert.Equal(expectedProjectTitle, project.Title);
        }

        [Fact]
        public async Task Can_delete_project_by_id()
        {
            //arrange 
            var expectedProjectCount = 0;
            string testProjectTitle = "Test project";
            var Client = Utilities<Startup>.CreateClient();
            var token = await Utilities<Startup>.RegisterandLoginUser("ghost", "test", Client);
            //Attach bearer token 
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Utilities<Startup>.StripTokenValue(token));
            var userName = Utilities<Startup>.StripUserNameValue(token);
            var addRequest = new
            {
                Url = $"api/projects/{userName}",
                Body = new
                {
                    id = 0,
                    Title = testProjectTitle,
                }
            };
            var addResult = await Client.PostAsync(addRequest.Url, ContentHelper.GetStringContent(addRequest.Body));
            var addProjectResponse = JsonConvert.DeserializeObject<ProjectResponse>(await addResult.Content.ReadAsStringAsync());
            //act
            var deleteRequest = new
            {
                Url = $"api/projects/{addProjectResponse.Id}"
            };
            
            var deleteResponse = await Client.DeleteAsync(deleteRequest.Url);
            //assert

            var getRequest = new
            {
                Url = $"api/projects/{userName}"
            };
            var getResponse = await Client.GetAsync(getRequest.Url);
            var projectResponse = JsonConvert.DeserializeObject<List<ProjectResponse>>(await getResponse.Content.ReadAsStringAsync());
            Assert.Equal(expectedProjectCount, projectResponse.Count);
        }

        [Fact]
        public async Task Can_get_project_by_project_Id()
        {
            //arrange
            string testProjectTitle = "Test project";
            var Client = Utilities<Startup>.CreateClient();
            var token = await Utilities<Startup>.RegisterandLoginUser("ghost", "test", Client);
            //Attach bearer token 
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Utilities<Startup>.StripTokenValue(token));
            var userName = Utilities<Startup>.StripUserNameValue(token);

            var addRequest = new
            {
                Url = $"api/projects/{userName}",
                Body = new
                {
                    id = 0,
                    Title = testProjectTitle,
                }
            };
            var addResult = await Client.PostAsync(addRequest.Url, ContentHelper.GetStringContent(addRequest.Body));
            var addProjectResponse = JsonConvert.DeserializeObject<ProjectResponse>(await addResult.Content.ReadAsStringAsync());

            //act
            var getRequest = new
            {
                Url = $"api/projects/GetForProjectId/{addProjectResponse.Id}"
            };
            var getResponse = await Client.GetAsync(getRequest.Url);
            //assert
            var projectResponse = JsonConvert.DeserializeObject<ProjectResponse>(await getResponse.Content.ReadAsStringAsync());
            Assert.Equal(projectResponse.Title, testProjectTitle);
        }

        [Fact]
        public async Task Add_project_with_title()
        {
            //arrange
            string testProjectTitle="Test project";
            var Client = Utilities<Startup>.CreateClient();
            var token = await Utilities<Startup>.RegisterandLoginUser("ghost", "test", Client);
            //Attach bearer token 
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Utilities<Startup>.StripTokenValue(token));
            var userName = Utilities<Startup>.StripUserNameValue(token);

            var addRequest = new
            {
                Url=$"api/projects/{userName}",
                Body = new
                {
                    id=0,
                    Title=testProjectTitle,
                }
            };

            var getRequest = new
            {
                Url = $"api/projects/{userName}"
            };
            
            //act
            var addResult = await Client.PostAsync(addRequest.Url, ContentHelper.GetStringContent(addRequest.Body));

            //assert
            Assert.Equal(HttpStatusCode.Created, addResult.StatusCode);
            var getResponse = await Client.GetAsync(getRequest.Url);
            var projectResponse = JsonConvert.DeserializeObject<List<ProjectResponse>>(await getResponse.Content.ReadAsStringAsync());
            Assert.Equal(testProjectTitle,projectResponse[0].Title);
        }
    }
}
