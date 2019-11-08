using System.Collections.Generic;

namespace CrystalCards.Api.Dtos
{
    public class ProjectResponse
    {
        public  ProjectResponse()
        {
            Cards=new List<CardResponse>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public List<CardResponse> Cards { get; set; }
    }
}