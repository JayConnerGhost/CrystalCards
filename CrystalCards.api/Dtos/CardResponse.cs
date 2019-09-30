using System.Collections.Generic;

namespace CrystalCards.Api.Dtos
{
    public class CardResponse
    {
        public CardResponse()
        {
            NPPoints = new List<NPPointResponse>();
        }

        public string Title { get; set; }
       
        public string Description { get; set; }

        public IList<NPPointResponse> NPPoints { get; set; }
        public int Id { get; set; }
    }
}