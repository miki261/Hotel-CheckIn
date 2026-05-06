using Hotel_CheckIn.Data;
using Hotel_CheckIn.Models;
using System.Collections.Generic;
using System.Linq;

namespace Hotel_CheckIn.Services
{
    public class GuestRepository
    {
        public void Add(Guest guest)
        {
            using var db = new HotelDbContext();
            db.Guests.Add(guest);
            db.SaveChanges();
        }

        public List<Guest> GetCurrentReservations()
        {
            using var db = new HotelDbContext();
            return db.Guests
                     .Where(g => !g.IsCheckedOut)
                     .OrderBy(g => g.RoomNumber)
                     .ToList();
        }

        public List<Guest> GetHistory()
        {
            using var db = new HotelDbContext();
            return db.Guests
                     .Where(g => g.IsCheckedOut)
                     .OrderByDescending(g => g.CheckOutDate)
                     .ToList();
        }

        public Guest? GetById(int id)
        {
            using var db = new HotelDbContext();
            return db.Guests.FirstOrDefault(g => g.Id == id);
        }

        public void Update(Guest guest)
        {
            using var db = new HotelDbContext();
            db.Guests.Update(guest);
            db.SaveChanges();
        }
    }
}