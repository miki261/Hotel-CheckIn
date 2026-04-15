using Hotel_CheckIn.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Hotel_CheckIn.Data
{
    public class HotelDbContext : DbContext
    {
        public DbSet<Guest> Guests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Hotel.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }
    }
}