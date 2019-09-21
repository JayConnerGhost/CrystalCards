using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CrystalCards.Models;

namespace CrystalCards.Api.Dtos
{
    public class UpdateCardRequest
    {
        public  UpdateCardRequest()
        {
            NPPoints=new List<NPPoint>();
        }
        [Required]
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }

        public IList<NPPoint> NPPoints { get; set; }
    }
}