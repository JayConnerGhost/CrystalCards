using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrystalCards.Api.Dtos
{
    public class NewCardRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }

    }
}
