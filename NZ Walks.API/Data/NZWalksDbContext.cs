using Microsoft.EntityFrameworkCore;
using NZ_Walks.API.Models.Domain;

namespace NZ_Walks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }

        public DbSet<Walk>  Walks { get; set; }
            base.OnModelCreating(modelBuilder);



          

    }
}
