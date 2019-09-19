using System;
using System.Collections.Generic;

namespace CrystalCards.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IList<NPPoint> Positives { get; set; }
    }
}
