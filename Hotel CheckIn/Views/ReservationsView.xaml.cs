using System.Windows;
using System.Windows.Controls;
using Hotel_CheckIn.Models;
using Hotel_CheckIn.Services;

namespace Hotel_CheckIn.Views
{
    public partial class ReservationsView : UserControl
    {
        private readonly GuestService _guestService = new GuestService();

        public ReservationsView()
        {
            InitializeComponent();
            LoadReservations();
        }

        private void LoadReservations()
        {
            ReservationsList.ItemsSource = null;
            ReservationsList.ItemsSource = _guestService.GetCurrentReservations();
        }

        private void CheckOut_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Guest guest)
            {
                var result = MessageBox.Show(
                    $"Check out guest '{guest.FullName}' from room {guest.RoomNumber}?",
                    "Confirm Check-Out",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    _guestService.CheckOutGuest(guest.Id);
                    LoadReservations();
                }
            }
        }
    }
}