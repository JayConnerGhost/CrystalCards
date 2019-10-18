using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CrystalCards.Api.Dtos
{
    public class NewCardRequest
    {
        public NewCardRequest()
        {
            NPPoints=new List<NPPointRequest>();
            ActionPoints=new List<ActionPointRequest>();
            Links=new List<LinkRequest>();
        }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public int Order { get; set; }
        public IList<NPPointRequest> NPPoints { get; set; }
        public IList<ActionPointRequest> ActionPoints { get; set; }
        public IList<LinkRequest> Links { get; set; }
    }
}

