using System;
using System.Windows.Controls;
using System.Windows.Data;
using Hotel_CheckIn.Models;
using Hotel_CheckIn.Services;

namespace Hotel_CheckIn.Views
{
    public partial class HistoryView : UserControl
    {
        private readonly ReservationService _reservationService = new ReservationService();

        public HistoryView()
        {
            InitializeComponent();
            LoadHistory();
        }

        private void LoadHistory()
        {
            HistoryList.ItemsSource = null;
            HistoryList.ItemsSource = _reservationService.GetHistory();

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(HistoryList.ItemsSource);
            view.Filter = HistoryFilter;
        }

        private bool HistoryFilter(object item)
        {
            if (item is not Guest guest)
                return false;

            string searchText = SearchHistoryTextBox.Text?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(searchText))
                return true;

            return guest.FullName.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                   guest.IdPassport.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                   guest.Email.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                   guest.RoomNumber.Contains(searchText, StringComparison.OrdinalIgnoreCase);
        }

        private void SearchHistoryTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(HistoryList.ItemsSource)?.Refresh();
        }
    }
}