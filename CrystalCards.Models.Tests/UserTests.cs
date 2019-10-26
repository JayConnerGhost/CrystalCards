using System;
using System.Threading.Tasks;
using Xunit;

namespace CrystalCards.Models
{
}

namespace CrystalCards.Models.Tests
{
    public class UserTests
    {
        [Fact]
        public void User_can_be_a_administrator()
        {
            //arrange 
            var user=new User();

            //act
            user.Roles.Add(new CustomRole {Name = Role.Administrator});
            //assert

            Assert.Equal(user.Roles[0].Name, Role.Administrator);
        }
    }

    
}
