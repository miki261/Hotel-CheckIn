using System;
using System.Windows;
using System.Windows.Controls;
using Hotel_CheckIn.Models;
using Hotel_CheckIn.Services;

namespace Hotel_CheckIn.Views
{
    public partial class NewGuestView : UserControl
    {
        private readonly GuestService _guestService = new GuestService();

        public NewGuestView()
        {
            InitializeComponent();

            CheckInDatePicker.SelectedDate = DateTime.Today;
            CheckOutDatePicker.SelectedDate = DateTime.Today.AddDays(1);
            CheckInTimeComboBox.SelectedIndex = 0;
            CheckOutTimeComboBox.SelectedIndex = 0;
        }

        private void CheckIn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) ||
                string.IsNullOrWhiteSpace(IdTextBox.Text) ||
                string.IsNullOrWhiteSpace(RoomTextBox.Text) ||
                CheckInDatePicker.SelectedDate == null ||
                CheckOutDatePicker.SelectedDate == null ||
                CheckInTimeComboBox.SelectedItem == null ||
                CheckOutTimeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            string checkInTimeText = ((ComboBoxItem)CheckInTimeComboBox.SelectedItem).Content.ToString()!;
            string checkOutTimeText = ((ComboBoxItem)CheckOutTimeComboBox.SelectedItem).Content.ToString()!;

            DateTime checkInDateTime = CombineDateAndTime(CheckInDatePicker.SelectedDate.Value, checkInTimeText);
            DateTime checkOutDateTime = CombineDateAndTime(CheckOutDatePicker.SelectedDate.Value, checkOutTimeText);

            if (checkOutDateTime <= checkInDateTime)
            {
                MessageBox.Show("Check-out must be after check-in.");
                return;
            }

            var guest = new Guest
            {
                FullName = NameTextBox.Text,
                IdPassport = IdTextBox.Text,
                RoomNumber = RoomTextBox.Text,
                CheckInDate = checkInDateTime,
                CheckOutDate = checkOutDateTime,
                IsCheckedOut = false
            };

            _guestService.AddGuest(guest);

            NameTextBox.Text = "";
            IdTextBox.Text = "";
            RoomTextBox.Text = "";
            CheckInDatePicker.SelectedDate = DateTime.Today;
            CheckOutDatePicker.SelectedDate = DateTime.Today.AddDays(1);
            CheckInTimeComboBox.SelectedIndex = 0;
            CheckOutTimeComboBox.SelectedIndex = 0;

            MessageBox.Show("Guest saved successfully.");
        }

        private DateTime CombineDateAndTime(DateTime date, string timeText)
        {
            TimeSpan time = TimeSpan.Parse(timeText);
            return date.Date + time;
        }
    }
}