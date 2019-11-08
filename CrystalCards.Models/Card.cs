using System;
using System.Collections;
using System.Collections.Generic;

namespace CrystalCards.Models
{

    public class UserProject
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
    public class Card
    {
        public Card()
        {
            Points=new List<NPPoint>();
            ActionPoints=new List<ActionPoint>();
            Links=new List<Link>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IList<NPPoint> Points { get; set; }
        public IList<ActionPoint> ActionPoints { get; set; }
        public IList<Link> Links { get; set; }
        public int Order { get; set; }
    }
}
