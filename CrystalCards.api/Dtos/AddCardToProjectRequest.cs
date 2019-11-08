using System.ComponentModel.DataAnnotations;

namespace CrystalCards.Api.Dtos
{
    public class AddCardToProjectRequest
    {
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public int CardId { get; set; }
    }

    public class RemoveCardFromProjectRequest
    {
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public int CardId { get; set; }
    }
}