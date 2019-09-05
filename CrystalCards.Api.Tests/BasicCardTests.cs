using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;


namespace CrystalCards.Api.Tests
{
   
        public static class ContentHelper
        {
            public static StringContent GetStringContent(object obj)
                => new StringContent(JsonConvert.SerializeObject(obj), Encoding.Default, "application/json");
        }
    
    public class BasicCardTests : CrystalCards.Api.Tests.HttpTestBase
    {
        public BasicCardTests() : base()
        {
        }

        [Fact]
        public async Task Receive_a_created_status_code_adding_card()
        {
            //Arrange
            //Set up a post 
            var request = new
            {
                Url = "api/cards",
                Body = new
                {
                   Title="Test Card 1",
                   Description="Test Card 1 description"
                }
            };
            //Set up the base class to host the http in memory
            var response = await Client.PostAsync(request.Url,ContentHelper.GetStringContent(request.Body));
            //Act
            //HttpStatusCode receivedStatusCode;

            var stringResponse =  response.StatusCode;
            //Assert


        }
    }
}
