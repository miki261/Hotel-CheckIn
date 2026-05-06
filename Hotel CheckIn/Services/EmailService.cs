using System;
using System.Diagnostics;
using System.Windows;

namespace Hotel_CheckIn.Services
{
    public class EmailService
    {
        public void OpenCheckInEmail(string toEmail, string guestName, string roomNumber)
        {
            string subject = "Welcome to our hotel";
            string body =
                $"Dear {guestName},\n\n" +
                $"Thank you for checking in with us. Your room number is {roomNumber}.\n" +
                $"We hope you enjoy your stay.\n\n" +
                $"Best regards,\nHotel Check-In";

            OpenEmailDraft(toEmail, subject, body);
        }

        public void OpenCheckOutEmail(string toEmail, string guestName, string roomNumber)
        {
            string subject = "Thank you for staying with us";
            string body =
                $"Dear {guestName},\n\n" +
                $"Thank you for staying with us in room {roomNumber}.\n" +
                $"We hope you had a pleasant stay and would love to welcome you again.\n\n" +
                $"Best regards,\nHotel Check-In";

            OpenEmailDraft(toEmail, subject, body);
        }

        private void OpenEmailDraft(string to, string subject, string body)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(to))
                    return;

                string mailto =
                    $"mailto:{to}" +
                    $"?subject={Uri.EscapeDataString(subject)}" +
                    $"&body={Uri.EscapeDataString(body)}";

                Process.Start(new ProcessStartInfo(mailto)
                {
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Email could not be opened:\n" + ex.Message);
            }
        }
    }
}