using System.ComponentModel.DataAnnotations;

namespace CrystalCards.Api.Dtos
{
    public class NewNPPointRequest
    {
        [Required]
        public int CardId { get; set; }

        [Required]
        public string Direction { get; set; }
    }
}