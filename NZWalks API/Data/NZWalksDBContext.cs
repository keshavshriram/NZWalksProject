using Microsoft.EntityFrameworkCore;
using NZWalks_API.Models.Domain;

namespace NZWalks_API.Data
{
    public class NZWalksDBContext : DbContext
    {
        public NZWalksDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        { 

        }

        public DbSet<Dificulty> Dificulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
    }
}
