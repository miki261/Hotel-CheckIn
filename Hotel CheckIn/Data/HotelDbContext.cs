using Hotel_CheckIn.Models;
using Microsoft.EntityFrameworkCore;

namespace Hotel_CheckIn.Data
{
    public class HotelDbContext : DbContext
    {
        public DbSet<Guest> Guests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DatabasePaths.MainDatabasePath}");
        }
    }
}