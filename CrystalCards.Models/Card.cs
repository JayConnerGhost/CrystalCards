using System;
using System.Collections.Generic;

namespace CrystalCards.Models
{
    public class Card
    {
        public Card()
        {
            Points=new List<NPPoint>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IList<NPPoint> Points { get; set; }
        public int Order { get; set; }
    }
}
