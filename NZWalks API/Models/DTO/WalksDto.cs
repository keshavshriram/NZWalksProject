using NZWalks_API.Models.Domain;

namespace NZWalks_API.Models.DTO
{
    public class WalksDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        public Guid DificultyId { get; set; }
        public Guid RegionId { get; set; }



        public DificultyDto Dificulty { get; set; }
        public RegionDto Region { get; set; }
    }
}
