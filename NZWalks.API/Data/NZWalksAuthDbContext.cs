using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Data
{
    public class NZWalksAuthDbContext : IdentityDbContext
    {
        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var readerRoleId = "8588c8ae-a02d-4a63-99ff-00e7655a06da";
            var writerRoleId = "fb51b46b-9bf4-45dc-9b7c-7360e5d09dd4";
            var roles = new List<IdentityRole>
            {
                new IdentityRole{
                    Id=readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()

                    },

                new IdentityRole{
                    Id=writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()

                    }
            };
        
            builder.Entity<IdentityRole>().HasData(roles);// qekjo ebon seed nese s ekzistojne already
        }
    }
}
