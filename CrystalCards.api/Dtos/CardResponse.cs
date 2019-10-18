using System.Collections.Generic;
using CrystalCards.Models;

namespace CrystalCards.Api.Dtos
{
    public class CardResponse
    {
        public CardResponse()
        {
            NPPoints = new List<NPPointResponse>();
            ActionPoints=new List<ActionPointResponse>();
            Links=new List<LinkResponse>();
        }

        public string Title { get; set; }
       
        public string Description { get; set; }

        public IList<NPPointResponse> NPPoints { get; set; }
        public IList<ActionPointResponse> ActionPoints { get; set; }
        public IList<LinkResponse> Links { get; set; }
        public int Id { get; set; }
        public int Order { get; set; }
    }
}