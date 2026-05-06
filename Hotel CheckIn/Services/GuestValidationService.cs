using System;
using System.Text.RegularExpressions;

namespace Hotel_CheckIn.Services
{
    public class GuestValidationService
    {
        public bool ValidateGuestInput(
            string fullName,
            string idPassport,
            string roomNumber,
            string email,
            DateTime? checkInDate,
            DateTime? checkOutDate,
            string? checkInTime,
            string? checkOutTime,
            out string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(fullName) ||
                string.IsNullOrWhiteSpace(idPassport) ||
                string.IsNullOrWhiteSpace(roomNumber) ||
                string.IsNullOrWhiteSpace(email) ||
                checkInDate == null ||
                checkOutDate == null ||
                string.IsNullOrWhiteSpace(checkInTime) ||
                string.IsNullOrWhiteSpace(checkOutTime))
            {
                errorMessage = "Please fill all fields.";
                return false;
            }

            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                errorMessage = "Please enter a valid email address.";
                return false;
            }

            DateTime checkIn = CombineDateAndTime(checkInDate.Value, checkInTime);
            DateTime checkOut = CombineDateAndTime(checkOutDate.Value, checkOutTime);

            if (checkOut <= checkIn)
            {
                errorMessage = "Check-out must be after check-in.";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }

        public DateTime CombineDateAndTime(DateTime date, string timeText)
        {
            TimeSpan time = TimeSpan.Parse(timeText);
            return date.Date + time;
        }
    }
}