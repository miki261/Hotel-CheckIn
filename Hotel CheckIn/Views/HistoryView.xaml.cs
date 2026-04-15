using System.Windows.Controls;
using Hotel_CheckIn.Services;

namespace Hotel_CheckIn.Views
{
    public partial class HistoryView : UserControl
    {
        private readonly GuestService _guestService = new GuestService();

        public HistoryView()
        {
            InitializeComponent();
            HistoryList.ItemsSource = _guestService.GetHistory();
        }
    }
}