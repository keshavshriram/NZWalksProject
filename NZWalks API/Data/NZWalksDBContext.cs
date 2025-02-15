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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seeding data for dificulties

            var dificulties = new List<Dificulty>() {
                new Dificulty(){
                    Id = Guid.Parse("2549b56a-ae0f-41f1-b716-826d3af186ec"),
                    Name = "Easy"
                }
                ,
                new Dificulty(){ 
                    Id = Guid.Parse("f415543f-9550-4aea-8e4a-2a948a1c4d3b"),
                    Name = "Medium"
                }
                ,
                new Dificulty(){ 
                    Id = Guid.Parse("89960b1e-7b20-481b-a4c6-40e0b528823b"),
                    Name = "Hard"
                }
            };

            //Seeding data into database 
            modelBuilder.Entity<Dificulty>().HasData(dificulties);


            var regions = new List<Region>() {
                new Region(){ 
                    Id = Guid.Parse("aa86736f-7d4f-4ad6-8aaa-c60da94865bb"),
                    Name = "Pune",
                    Code = "411041",
                    RegionImageUrl = "puneUrl.com"
                },
                new Region(){ 
                    Id = Guid.Parse("b95c7344-c628-4f8c-a020-7f17c09492d3"),
                    Name = "Aahilyanagar",
                    Code = "414401",
                    RegionImageUrl = "aahilyaNagar.com"
                },
                new Region(){
                    Id = Guid.Parse("0b6685f4-361e-4f56-a356-d090702e5c6f"),
                    Name = "Karjat",
                    Code = "414402",
                    RegionImageUrl = "aahilyaNagar.com"
                },
                new Region(){
                    Id = Guid.Parse("abd81311-e6c6-4f25-a387-7b3d77d659f8"),
                    Name = "Himachal Pradesh",
                    Code = "1234567",
                    RegionImageUrl = "aahilyaNagar.com"
                }

            };

            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
