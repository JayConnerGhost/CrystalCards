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
            Roles=new List<CustomRole>();
            Projects=new List<UserProject>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public IList<Card> Cards { get; set; }
        public List<CustomRole> Roles { get; set; }
        public List<UserProject> Projects { get; set; }
    }
}
