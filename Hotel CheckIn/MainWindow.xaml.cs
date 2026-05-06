using System.Windows;
using System.Windows.Controls;
using Hotel_CheckIn.Views;

namespace Hotel_CheckIn
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ShowView(new DashboardView());
        }

        private void ShowView(UserControl view)
        {
            MainContent.Content = view;
        }

        private void Dashboard_Click(object sender, RoutedEventArgs e)
        {
            ShowView(new DashboardView());
        }

        private void NewGuest_Click(object sender, RoutedEventArgs e)
        {
            ShowView(new NewGuestView());
        }

        private void Reservations_Click(object sender, RoutedEventArgs e)
        {
            ShowView(new ReservationsView());
        }

        private void History_Click(object sender, RoutedEventArgs e)
        {
            ShowView(new HistoryView());
        }

        private void RoomStatus_Click(object sender, RoutedEventArgs e)
        {
            ShowView(new RoomStatusView());
        }
    }
}