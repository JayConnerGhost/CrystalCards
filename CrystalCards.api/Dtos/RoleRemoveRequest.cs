using System.ComponentModel.DataAnnotations;

namespace CrystalCards.Api.Dtos
{
    public class RoleRemoveRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}