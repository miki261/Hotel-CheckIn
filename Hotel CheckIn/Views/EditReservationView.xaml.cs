using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Hotel_CheckIn.Models;
using Hotel_CheckIn.Services;

namespace Hotel_CheckIn.Views
{
    public partial class EditReservationView : Window
    {
        private readonly ReservationService _reservationService = new ReservationService();
        private Guest _currentGuest;

        public EditReservationView(Guest guest)
        {
            InitializeComponent();
            _currentGuest = guest;
            LoadGuestData();
        }

        private void LoadGuestData()
        {
            NameTextBox.Text = _currentGuest.FullName;
            RoomTextBox.Text = _currentGuest.RoomNumber;

            CheckOutDatePicker.SelectedDate = _currentGuest.CheckOutDate.Date;

            string timeString = _currentGuest.CheckOutDate.ToString("HH:00");
            foreach (ComboBoxItem item in CheckOutTimeComboBox.Items)
            {
                if (item.Content.ToString() == timeString)
                {
                    CheckOutTimeComboBox.SelectedItem = item;
                    break;
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                MessageBox.Show("Name cannot be empty.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(RoomTextBox.Text) || !RoomTextBox.Text.All(char.IsDigit))
            {
                MessageBox.Show("Room Number must contain only numbers.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string? checkOutTime = (CheckOutTimeComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
            string checkInTime = _currentGuest.CheckInDate.ToString("HH:mm");

            bool success = _reservationService.TryEditReservation(
                _currentGuest.Id,
                NameTextBox.Text,
                _currentGuest.IdPassport,
                RoomTextBox.Text,
                _currentGuest.Email,
                _currentGuest.CheckInDate.Date,
                CheckOutDatePicker.SelectedDate,
                checkInTime,
                checkOutTime,
                out string message
            );

            if (success)
            {
                MessageBox.Show(message, "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}