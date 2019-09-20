using System.Threading.Tasks;
using CrystalCards.api;
using CrystalCards.Models;
using Newtonsoft.Json;

namespace CrystalCards.Api.Tests
{
    public static class Utilities<T> where T :class
    {
        
        public static async Task<int> SetupACardReturnId(string description, string title, CustomWebApplicationFactory<T> factory)
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
            var Client = factory.CreateClient();
            
            var response = await Client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            
            var card = JsonConvert.DeserializeObject<Card>(await response.Content.ReadAsStringAsync());
            return card.Id;
        }
    }
}