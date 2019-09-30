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
        }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }

        public IList<NPPointRequest> NPPoints { get; set; }
    }
}

