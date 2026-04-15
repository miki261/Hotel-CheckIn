using Hotel_CheckIn.Data;
using Hotel_CheckIn.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hotel_CheckIn.Services
{
    public class GuestService
    {
        public void AddGuest(Guest guest)
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

        public void CheckOutGuest(int guestId)
        {
            using var db = new HotelDbContext();

            var guest = db.Guests.FirstOrDefault(g => g.Id == guestId);
            if (guest == null)
                return;

            guest.IsCheckedOut = true;
            guest.CheckOutDate = DateTime.Now;

            db.SaveChanges();
        }
    }
}