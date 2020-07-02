using System.ComponentModel.DataAnnotations;

namespace CrystalCards.Api.Dtos
{
    public class UserForRegisterRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string SecondName { get; set; }

        [Required]
        [StringLength(8,MinimumLength = 4, ErrorMessage = "you must specify password between 4 and 8 charactors")]
        public string Password { get; set; }


    }
}