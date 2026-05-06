using Hotel_CheckIn.Data;
using Hotel_CheckIn.Models;
using System;
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

            var existingGuest = db.Guests.FirstOrDefault(g => g.Id == guest.Id);
            if (existingGuest == null)
                return;

            existingGuest.FullName = guest.FullName;
            existingGuest.IdPassport = guest.IdPassport;
            existingGuest.Email = guest.Email;
            existingGuest.RoomNumber = guest.RoomNumber;
            existingGuest.CheckInDate = guest.CheckInDate;
            existingGuest.CheckOutDate = guest.CheckOutDate;
            existingGuest.IsCheckedOut = guest.IsCheckedOut;

            db.SaveChanges();
        }

        public bool RoomHasOverlappingReservation(string roomNumber, DateTime newCheckIn, DateTime newCheckOut, int? ignoreGuestId = null)
        {
            using var db = new HotelDbContext();

            return db.Guests.Any(g =>
                !g.IsCheckedOut &&
                g.RoomNumber == roomNumber &&
                (!ignoreGuestId.HasValue || g.Id != ignoreGuestId.Value) &&
                newCheckIn < g.CheckOutDate &&
                g.CheckInDate < newCheckOut);
        }
    }
}