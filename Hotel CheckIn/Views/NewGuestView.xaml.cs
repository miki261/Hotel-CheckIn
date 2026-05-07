using System;
using System.Linq;
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
            if (string.IsNullOrWhiteSpace(IdTextBox.Text) || !IdTextBox.Text.All(char.IsDigit))
            {
                MessageBox.Show("ID / Passport Number must contain only numbers.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(RoomTextBox.Text) || !RoomTextBox.Text.All(char.IsDigit))
            {
                MessageBox.Show("Room Number must contain only numbers.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

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