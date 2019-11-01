using System.ComponentModel.DataAnnotations;

namespace CrystalCards.Api.Dtos
{
    public class RoleAssignmentRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}