using CrystalCards.Models;

namespace CrystalCards.Api.Dtos
{
    public class UserResponse
    {
        public string Username { get; set; }
        public CustomRole[] Roles { get; set; }
    }
}