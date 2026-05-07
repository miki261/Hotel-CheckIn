using System;
using System.Windows;
using Hotel_CheckIn.Views;

namespace Hotel_CheckIn
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainContent.Content = new DashboardView();

            this.Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (GlobalSettings.BackupEnabled)
            {
                try
                {
                    var backupService = new Services.DatabaseBackupService();
                    backupService.BackupDatabase();
                }
                catch
                {
                }
            }
        }

        private void Dashboard_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new DashboardView();
        }

        private void NewGuest_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new NewGuestView();
        }

        private void Reservations_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ReservationsView();
        }

        private void History_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new HistoryView();
        }

        private void RoomStatus_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new RoomStatusView();
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new SettingsView();
        }
    }
}