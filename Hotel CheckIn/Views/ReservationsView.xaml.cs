using System.Windows;
using System.Windows.Controls;
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
    }
}