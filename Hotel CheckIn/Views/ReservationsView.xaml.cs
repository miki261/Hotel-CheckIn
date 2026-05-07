using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Hotel_CheckIn.Models;
using Hotel_CheckIn.Services;

namespace Hotel_CheckIn.Views
{
    public partial class ReservationsView : UserControl
    {
        private readonly ReservationService _reservationService = new ReservationService();

        public ReservationsView()
        {
            InitializeComponent();
            LoadReservations();
        }

        private void LoadReservations()
        {
            ReservationsList.ItemsSource = null;
            ReservationsList.ItemsSource = _reservationService.GetCurrentReservations();

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ReservationsList.ItemsSource);
            view.Filter = ReservationFilter;
        }

        private bool ReservationFilter(object item)
        {
            if (item is not Guest guest)
                return false;

            string searchText = SearchReservationsTextBox.Text?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(searchText))
                return true;

            return guest.FullName.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                   guest.IdPassport.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                   guest.Email.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                   guest.RoomNumber.Contains(searchText, StringComparison.OrdinalIgnoreCase);
        }

        private void SearchReservationsTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ReservationsList.ItemsSource)?.Refresh();
        }

        private void CheckOut_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button button || button.DataContext is not Guest guest)
                return;

            var result = MessageBox.Show(
                $"Check out guest '{guest.FullName}' from room {guest.RoomNumber}?",
                "Confirm Check-Out",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
                return;

            bool success = _reservationService.CheckOutGuest(guest.Id);

            if (success)
            {
                LoadReservations();
                MessageBox.Show("Guest checked out successfully.");
            }
            else
            {
                MessageBox.Show("Guest could not be checked out.");
            }
        }

        private void EditReservation_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button button || button.DataContext is not Guest guest)
                return;

            var editWindow = new EditReservationView(guest);
            bool? result = editWindow.ShowDialog();

            if (result == true)
            {
                LoadReservations();
            }
        }
    }
}