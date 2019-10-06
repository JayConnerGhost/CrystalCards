using System;
using System.Collections;
using System.Collections.Generic;

namespace CrystalCards.Models
{
    public class Card
    {
        public Card()
        {
            Points=new List<NPPoint>();
            ActionPoints=new List<ActionPoint>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IList<NPPoint> Points { get; set; }
        public IList<ActionPoint> ActionPoints { get; set; }
        public int Order { get; set; }
    }
}
