namespace Hotel_CheckIn.Models
{
    public class Guest
    {
        public int Id { get; set; }
        public string FullName { get; set; } = "";
        public string IdPassport { get; set; } = "";
        public string RoomNumber { get; set; } = "";
        public string Email { get; set; } = "";
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public bool IsCheckedOut { get; set; }
    }
}