namespace NZWalks_API.Models.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        public Guid DificultyId { get; set; }
        public Guid RegionId { get; set; }


        //Navigation properties 
        public Dificulty Dificulty { get; set; }
        public Region Region { get; set; }
    }
}
