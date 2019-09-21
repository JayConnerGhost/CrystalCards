using System.Threading.Tasks;
using CrystalCards.api;
using CrystalCards.Models;
using Newtonsoft.Json;
using Xunit;

namespace CrystalCards.Api.Tests
{
    public class ProConsCardTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private CustomWebApplicationFactory<Startup> _factory = new CustomWebApplicationFactory<Startup>();

        [Fact]
        public async Task Can_add_a_Pro_to_a_card()
        {
            //Arrange 

            
            //act


            //Assert
            Assert.Equal(expectedCount,card.Positives.Count);
        }
    }
}