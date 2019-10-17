﻿using System.Net.Http.Headers;
using System.Threading.Tasks;
using CrystalCards.api;
using CrystalCards.Api.Dtos;
using CrystalCards.Api.Tests.Utils;
using Newtonsoft.Json;
using Xunit;

namespace CrystalCards.Api.Tests
{
    public class OrderValueTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        [Fact]
        public async Task Set_order_value_on_card()
        {
            //arrange
            string testCardTitle = "Edited Title";
            string testCardDescription = "Edited Description";
            var Client = Utilities<Startup>.CreateClient();
            var token = await Utilities<Startup>.RegisterandLoginUser("ghost", "test", Client);
            //Attach bearer token 
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Utilities<Startup>.StripTokenValue(token));
            int testOrder=1;
            var request = new
            {
                Url = $"api/cards",
                Body = new
                {

                    Title = testCardTitle,
                    Description = testCardDescription,
                    Order=testOrder,
                    NPPoints = new[]{
                        new NPPointRequest(){Direction="Negative", Description = "test negative"},
                        new NPPointRequest(){Direction="Negative", Description = "test negative2"}
                    }
                }
            };
            //act
            var result = await Client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            var newCard = JsonConvert.DeserializeObject<CardResponse>(await result.Content.ReadAsStringAsync());

            //assert
            var response = await Client.GetAsync(request.Url + "/" + newCard.Id);
            var card = JsonConvert.DeserializeObject<CardResponse>(await response.Content.ReadAsStringAsync());
            Assert.Equal(testOrder,card.Order);
        }
    }
}