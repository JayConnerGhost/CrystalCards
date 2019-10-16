using System.Net.Http;
using System.Threading.Tasks;
using CrystalCards.api;
using CrystalCards.Models;
using GST.Fake.Authentication.JwtBearer;
using Newtonsoft.Json;

namespace CrystalCards.Api.Tests.Utils
{
    public static class Utilities<T> where T :class
    {
        
        public static async Task<int> SetupACardReturnId(string description, string title, HttpClient client)
        {
            var request = new
            {
                Url = "api/cards",
                Body = new
                {
                    Title = title,
                    Description = description
                }
            };
            
            
            var response = await client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            
            var card = JsonConvert.DeserializeObject<Card>(await response.Content.ReadAsStringAsync());
              return card.Id;
        }

        public static HttpClient CreateClient()
        {
         CustomWebApplicationFactory<T> factory = new CustomWebApplicationFactory<T>();

        dynamic token = new System.Dynamic.ExpandoObject();
            token.sub = "SuperUserName";
            token.role = new[] { "role1", "role2" };
            token.email = "superuser@companytest.tld";

            var httpClient = factory.CreateClient();
            httpClient.SetFakeBearerToken((object)token);

            return httpClient;
        }
    }
}