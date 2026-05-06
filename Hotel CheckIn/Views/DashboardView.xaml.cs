using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using Hotel_CheckIn.Services;

namespace Hotel_CheckIn.Views
{
    public partial class DashboardView : UserControl
    {
        private readonly DashboardService _dashboardService = new DashboardService();
        private readonly DatabaseBackupService _backupService = new DatabaseBackupService();
        private readonly DatabaseRestoreCoordinator _restoreCoordinator = new DatabaseRestoreCoordinator();
        private readonly BackupMetadataService _backupMetadataService = new BackupMetadataService();

        public DashboardView()
        {
            InitializeComponent();
            LoadStats();
            LoadLastBackupDate();
        }

        private void LoadStats()
        {
            var stats = _dashboardService.GetStats();

            ActiveReservationsText.Text = stats.ActiveReservations.ToString();
            CheckedOutTodayText.Text = stats.CheckedOutToday.ToString();
            TotalGuestsText.Text = stats.TotalGuests.ToString();
            OccupiedRoomsText.Text = stats.OccupiedRooms.ToString();
        }

        private void LoadLastBackupDate()
        {
            DateTime? lastBackup = _backupMetadataService.GetLastBackupDate();

            if (lastBackup.HasValue)
            {
                LastBackupText.Text = $"Last backup: {lastBackup.Value:dd.MM.yyyy HH:mm}";
            }
            else
            {
                LastBackupText.Text = "Last backup: Never";
            }
        }

        private void BackupDatabase_Click(object sender, RoutedEventArgs e)
        {
            bool success = _backupService.BackupDatabase();

            MessageBox.Show(
                success ? "Database backup created successfully." : "Backup cancelled or failed.");

            if (success)
            {
                LoadLastBackupDate();
            }
        }

        private void RestoreDatabase_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Restoring a backup will overwrite the current database. Continue?",
                "Confirm Restore",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes)
                return;

            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "Database Backup (*.db)|*.db"
            };

            if (dialog.ShowDialog() != true)
                return;

            _restoreCoordinator.SavePendingRestore(dialog.FileName);

            MessageBox.Show("The app will now close. Start it again to complete the restore.");
            Application.Current.Shutdown();
        }
    }
}