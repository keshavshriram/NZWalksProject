using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks_API.Data
{
    public class NZWalksAuthDbContext : IdentityDbContext
    {
        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : base(options)
        { 

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var roles = new List<IdentityRole> {
                new IdentityRole{
                    Id = "130fccb4-8e9a-4563-b064-434fd308c74d",
                    ConcurrencyStamp = "130fccb4-8e9a-4563-b064-434fd308c74d" ,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                new IdentityRole{
                    Id = "9ed88b97-d19e-41a0-8e43-ac552d47d965",
                    ConcurrencyStamp = "9ed88b97-d19e-41a0-8e43-ac552d47d965",
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
