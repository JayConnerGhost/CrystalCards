using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CrystalCards.api;
using CrystalCards.Models;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CrystalCards.Api.Tests.Utils
{
    public static class Utilities<T> where T :class
    {

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
        
        public static async Task<int> SetupACardReturnId(string description, string title, HttpClient client)
        {
            var request = new           {
                Url = "api/cards",
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
    }
}