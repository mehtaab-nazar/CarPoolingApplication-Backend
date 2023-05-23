

using CarPoolingApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace CarPoolingApplication.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<BookedRides> BookedRides { get; set; }
        public DbSet<OfferedRides> OfferedRides { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OfferedRides>().Property(ride => ride.Stops).HasConversion(stop => string.Join(',', stop), v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));
        }
    }
}
