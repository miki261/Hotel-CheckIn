namespace Hotel_CheckIn.Models
{
    public class DashboardStats
    {
        public int ActiveReservations { get; set; }
        public int CheckedOutToday { get; set; }
        public int TotalGuests { get; set; }
        public int OccupiedRooms { get; set; }
    }
}