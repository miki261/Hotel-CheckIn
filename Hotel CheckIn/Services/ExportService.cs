using Hotel_CheckIn.Models;
using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Hotel_CheckIn.Services
{
    public class ExportService
    {
        public bool ExportHistoryToCsv(List<Guest> guests)
        {
            if (guests == null || guests.Count == 0)
                return false;

            SaveFileDialog dialog = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                FileName = "GuestHistory.csv",
                DefaultExt = ".csv"
            };

            if (dialog.ShowDialog() != true)
                return false;

            StringBuilder csv = new StringBuilder();
            csv.AppendLine("FullName,IdPassport,Email,RoomNumber,CheckInDate,CheckOutDate,IsCheckedOut");

            foreach (var guest in guests)
            {
                csv.AppendLine(
                    $"\"{EscapeCsv(guest.FullName)}\"," +
                    $"\"{EscapeCsv(guest.IdPassport)}\"," +
                    $"\"{EscapeCsv(guest.Email)}\"," +
                    $"\"{EscapeCsv(guest.RoomNumber)}\"," +
                    $"\"{guest.CheckInDate:dd.MM.yyyy HH:mm}\"," +
                    $"\"{guest.CheckOutDate:dd.MM.yyyy HH:mm}\"," +
                    $"\"{guest.IsCheckedOut}\"");
            }

            File.WriteAllText(dialog.FileName, csv.ToString(), Encoding.UTF8);
            return true;
        }

        private string EscapeCsv(string? value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            return value.Replace("\"", "\"\"");
        }
    }
}