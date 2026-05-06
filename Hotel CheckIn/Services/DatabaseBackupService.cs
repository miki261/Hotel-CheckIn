using Hotel_CheckIn.Data;
using Microsoft.Data.Sqlite;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;

namespace Hotel_CheckIn.Services
{
    public class DatabaseBackupService
    {
        private readonly string _databasePath;
        private readonly BackupMetadataService _backupMetadataService;

        public DatabaseBackupService()
        {
            _databasePath = DatabasePaths.MainDatabasePath;
            _backupMetadataService = new BackupMetadataService();
        }

        public bool BackupDatabase()
        {
            if (!File.Exists(_databasePath))
            {
                MessageBox.Show("Database file was not found.");
                return false;
            }

            try
            {
                FlushWalToDatabase();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Backup preparation failed:\n" + ex.Message);
                return false;
            }

            SaveFileDialog dialog = new SaveFileDialog
            {
                Filter = "Database Backup (*.db)|*.db",
                FileName = $"GuestDatabase_Backup_{DateTime.Now:yyyyMMdd_HHmmss}.db"
            };

            if (dialog.ShowDialog() != true)
                return false;

            File.Copy(_databasePath, dialog.FileName, true);

            _backupMetadataService.SaveLastBackupDate(DateTime.Now);

            return true;
        }

        private void FlushWalToDatabase()
        {
            using var connection = new SqliteConnection($"Data Source={_databasePath}");
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "PRAGMA wal_checkpoint(FULL);";
            command.ExecuteNonQuery();
        }
    }
}