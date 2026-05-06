using System.Windows.Controls;
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
        }
    }
}