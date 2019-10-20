using System;
using System.Collections.Generic;
using System.Text;

namespace CrystalCards.Models
{
    public class User
    {
        public User()
        {
            Cards=new List<Card>();
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public IList<Card> Cards { get; set; }
    }
}
