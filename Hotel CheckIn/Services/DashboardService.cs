using Hotel_CheckIn.Data;
using Hotel_CheckIn.Models;
using System;
using System.Linq;

namespace Hotel_CheckIn.Services
{
    public class DashboardService
    {
        public DashboardStats GetStats()
        {
            using var db = new HotelDbContext();

            return new DashboardStats
            {
                ActiveReservations = db.Guests.Count(g => !g.IsCheckedOut),

                CheckedOutToday = db.Guests.Count(g =>
                    g.IsCheckedOut &&
                    g.CheckOutDate.Date == DateTime.Today),

                TotalGuests = db.Guests.Count(),

                OccupiedRooms = db.Guests
                    .Where(g => !g.IsCheckedOut)
                    .Select(g => g.RoomNumber)
                    .Distinct()
                    .Count()
            };
        }
    }
}