namespace NZWalks_API.Models.DTO
{
    public class WalkCreateDto
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        public Guid DificultyId { get; set; }
        public Guid RegionId { get; set; }

    }
}
