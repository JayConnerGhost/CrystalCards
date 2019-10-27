using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CrystalCards.api;
using CrystalCards.Data;
using CrystalCards.Models;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CrystalCards.Api.Tests.Utils
{
    public static class Utilities<T> where T :class
    {

        public static async Task<string> LoginUser(string password, string userName, HttpClient client)
        {
            var loginRequest = new
            {
                Url = "api/auth/login",

                Body = new
                {
                    username = userName,
                    password = password
                }
            };

            var stringContent = ContentHelper.GetStringContent(loginRequest.Body);
            var tokenResponse = await client.PostAsync(loginRequest.Url, stringContent);
            var jsonCompactSerializedString = await tokenResponse.Content.ReadAsStringAsync();
            return jsonCompactSerializedString;
        }
    




        public static async Task<string> RegisterandLoginUser(string password, string userName, HttpClient client)
        {
            var request = new
            {
                Url = "api/auth/register",
                Body = new
                {
                    username = userName,
                    password = password
                }
            };

            var loginRequest = new
            {
                Url = "api/auth/login",
                    
                Body = new
                {
                    username = userName,
                    password = password
                }
            };
            var response = await client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            var stringContent = ContentHelper.GetStringContent(loginRequest.Body);
            var tokenResponse = await client.PostAsync(loginRequest.Url, stringContent);
            var jsonCompactSerializedString= await tokenResponse.Content.ReadAsStringAsync();
            return jsonCompactSerializedString;
        }   
        
        public static async Task<int> SetupACardReturnId(string description, string title, HttpClient client,string userName)
        {
            var request = new           {
                Url = $"api/cards/{userName}",
                Body = new
                {
 
                    Title = title,
                    Description = description
                }
            };


            var stringContent = ContentHelper.GetStringContent(request.Body);
            var response = await client.PostAsync(request.Url, stringContent);
            var card = JsonConvert.DeserializeObject<Card>(await response.Content.ReadAsStringAsync());
              return card.Id;
        }

        public static HttpClient CreateClient()
        {
         CustomWebApplicationFactory<T> factory = new CustomWebApplicationFactory<T>();
         var httpClient = factory.CreateClient();
         return httpClient;
        }
       
        public static string StripTokenValue(string token)
        {
            var tokenValue = "";
            JObject jObject = JObject.Parse(token); 
            tokenValue = (string)jObject.SelectToken("token");
            return tokenValue;
        }

        public static string StripUserNameValue(string token)
        {
            var userName = "";
            JObject jObject = JObject.Parse(token);
            userName = (string)jObject.SelectToken("username");
            return userName;
        }

        public static async Task InitializeDbForTests(ApplicationDbContext db)
        {
            //TODO: Code in here to add ghostAdmin test as administrator
            //create repo passin password and username 
            //find user in _context //db and add administrator role
            var repository = new AuthRepository(db);
            var result= await repository.Register(new User(){Username = "ghostadmin",Roles = new List<CustomRole>() {new CustomRole(){Name = Role.Administrator}}}, "test");
        }
    }
}