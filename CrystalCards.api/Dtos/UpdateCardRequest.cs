using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CrystalCards.Models;

namespace CrystalCards.Api.Dtos
{
    public class UpdateCardRequest
    {
        public  UpdateCardRequest()
        {
            NPPoints=new List<NPPointRequest>();
            ActionPoints=new List<ActionPointRequest>();
            Links=new List<LinkRequest>();
        }
        [Required]
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }

        public IList<NPPointRequest> NPPoints { get; set; }
        public IList<ActionPointRequest> ActionPoints { get; set; }
        public int Order { get; set; }
        public IList<LinkRequest> Links { get; set; }
    }
}