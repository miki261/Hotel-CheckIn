using System;
using System.Windows;
using System.Windows.Controls;
using Hotel_CheckIn.Services;

namespace Hotel_CheckIn.Views
{
    public partial class NewGuestView : UserControl
    {
        private readonly ReservationService _reservationService = new ReservationService();

        public NewGuestView()
        {
            InitializeComponent();
            ResetForm();
        }

        private void CheckIn_Click(object sender, RoutedEventArgs e)
        {
            string? checkInTime = (CheckInTimeComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
            string? checkOutTime = (CheckOutTimeComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();

            bool success = _reservationService.TryCheckInGuest(
                NameTextBox.Text,
                IdTextBox.Text,
                RoomTextBox.Text,
                EmailTextBox.Text,
                CheckInDatePicker.SelectedDate,
                CheckOutDatePicker.SelectedDate,
                checkInTime,
                checkOutTime,
                out string message);

            MessageBox.Show(message);

            if (success)
            {
                ResetForm();
            }
        }

        private void ResetForm()
        {
            NameTextBox.Text = "";
            IdTextBox.Text = "";
            RoomTextBox.Text = "";
            EmailTextBox.Text = "";
            CheckInDatePicker.SelectedDate = DateTime.Today;
            CheckOutDatePicker.SelectedDate = DateTime.Today.AddDays(1);
            CheckInTimeComboBox.SelectedIndex = 0;
            CheckOutTimeComboBox.SelectedIndex = 0;
        }
    }
}