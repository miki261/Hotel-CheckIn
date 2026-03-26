using System.Windows;
using Hotel_CheckIn.Views;

namespace Hotel_CheckIn
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainContent.Content = new NewGuestView();
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
    }
}