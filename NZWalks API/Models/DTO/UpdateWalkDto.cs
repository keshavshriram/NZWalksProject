using System.ComponentModel.DataAnnotations;

namespace NZWalks_API.Models.DTO
{
    public class UpdateWalkDto
    {
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        [Required]
        [MaxLength(150)]
        public string Description { get; set; }
        [Range(1,100)]
        [Required]
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        [Required]
        public Guid DificultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }
    }
}
