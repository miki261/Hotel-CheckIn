using System;
using System.Globalization;
using System.IO;

namespace Hotel_CheckIn.Services
{
    public class BackupMetadataService
    {
        private readonly string _metadataPath;

        public BackupMetadataService()
        {
            _metadataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "last_backup.txt");
        }

        public void SaveLastBackupDate(DateTime backupDate)
        {
            File.WriteAllText(_metadataPath, backupDate.ToString("O"));
        }

        public DateTime? GetLastBackupDate()
        {
            if (!File.Exists(_metadataPath))
                return null;

            string content = File.ReadAllText(_metadataPath).Trim();

            if (DateTime.TryParse(
                content,
                null,
                DateTimeStyles.RoundtripKind,
                out DateTime parsedDate))
            {
                return parsedDate;
            }

            return null;
        }
    }
}