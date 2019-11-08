using System.Collections.Generic;

namespace CrystalCards.Models
{
    public class UserProject
    {
        public UserProject()
        {
            Cards=new List<Card>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Card> Cards { get; set; }
    }
}