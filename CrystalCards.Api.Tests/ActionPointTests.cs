using CrystalCards.api;
using Xunit;

namespace CrystalCards.Api.Tests
{
    public class ActionPointTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private CustomWebApplicationFactory<Startup> _factory = new CustomWebApplicationFactory<Startup>();
    }
}