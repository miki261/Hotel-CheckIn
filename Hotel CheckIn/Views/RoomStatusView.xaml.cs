using System.Windows.Controls;
using Hotel_CheckIn.Services;

namespace Hotel_CheckIn.Views
{
    public partial class RoomStatusView : UserControl
    {
        private readonly RoomStatusService _roomStatusService = new RoomStatusService();

        public RoomStatusView()
        {
            InitializeComponent();
            LoadRoomStatuses();
        }

        private void LoadRoomStatuses()
        {
            RoomStatusList.ItemsSource = _roomStatusService.GetRoomStatuses();
        }
    }
}