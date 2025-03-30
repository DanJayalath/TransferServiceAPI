using Microsoft.EntityFrameworkCore;
using TransferServiceAPI.Models;

namespace TransferServiceAPI.DataAccess
{
    public class TransferServiceDbContext : DbContext
    {
        public TransferServiceDbContext(DbContextOptions<TransferServiceDbContext> options)
            : base(options)
        {
        }

        public DbSet<Location> Locations { get; set; }
        public DbSet<LocationCategory> LocationCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LocationCategory>()
                .Property(lc => lc.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Location>()
                .HasOne(l => l.LocationCategory)
                .WithMany()
                .HasForeignKey(l => l.LocationCategoryId)
                .IsRequired();
        }
    }
}
