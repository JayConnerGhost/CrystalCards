﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CrystalCards.Api.Dtos
{
    public class NewCardRequest
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }

    }

    public class UpdateCardRequest
    {
        [Required]
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }
    }
}
