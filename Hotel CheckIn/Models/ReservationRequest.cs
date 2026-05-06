using System;

namespace Hotel_CheckIn.Models
{
    public class ReservationRequest
    {
        public string FullName { get; set; } = "";
        public string IdPassport { get; set; } = "";
        public string RoomNumber { get; set; } = "";
        public string Email { get; set; } = "";
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public string? CheckInTime { get; set; }
        public string? CheckOutTime { get; set; }
    }
}