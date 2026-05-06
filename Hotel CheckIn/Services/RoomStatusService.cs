using Hotel_CheckIn.Data;
using Hotel_CheckIn.Models;
using System.Collections.Generic;
using System.Linq;

namespace Hotel_CheckIn.Services
{
    public class RoomStatusService
    {
        public List<RoomStatusInfo> GetRoomStatuses()
        {
            using var db = new HotelDbContext();

            return db.Guests
                .Where(g => !g.IsCheckedOut)
                .OrderBy(g => g.RoomNumber)
                .Select(g => new RoomStatusInfo
                {
                    RoomNumber = g.RoomNumber,
                    Status = "Occupied",
                    GuestName = g.FullName
                })
                .ToList();
        }
    }
}