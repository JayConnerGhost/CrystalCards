using System;
using System.Collections.Generic;

namespace CrystalCards.Models
{
    public class Card
    {
        public Card()
        {
            Positives=new List<NPPoint>();
            Negatives = new List<NPPoint>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IList<NPPoint> Positives { get; set; }
        public IList<NPPoint> Negatives { get; set; }
    }
}
