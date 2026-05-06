using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Hotel_CheckIn.Models;
using Hotel_CheckIn.Services;

namespace Hotel_CheckIn.Views
{
    public partial class EditReservationWindow : Window
    {
        private readonly ReservationService _reservationService = new ReservationService();
        private readonly Guest _guest;

        public EditReservationWindow(Guest guest)
        {
            InitializeComponent();
            _guest = guest;
            LoadGuestData();
        }

        private void LoadGuestData()
        {
            NameTextBox.Text = _guest.FullName;
            IdTextBox.Text = _guest.IdPassport;
            EmailTextBox.Text = _guest.Email;
            RoomTextBox.Text = _guest.RoomNumber;

            CheckInDatePicker.SelectedDate = _guest.CheckInDate.Date;
            CheckOutDatePicker.SelectedDate = _guest.CheckOutDate.Date;

            SetComboBoxTime(CheckInTimeComboBox, _guest.CheckInDate.ToString("HH:mm"));
            SetComboBoxTime(CheckOutTimeComboBox, _guest.CheckOutDate.ToString("HH:mm"));
        }

        private void SetComboBoxTime(ComboBox comboBox, string timeText)
        {
            foreach (ComboBoxItem item in comboBox.Items)
            {
                if (item.Content?.ToString() == timeText)
                {
                    comboBox.SelectedItem = item;
                    return;
                }
            }

            comboBox.SelectedIndex = 0;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string? checkInTime = (CheckInTimeComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
            string? checkOutTime = (CheckOutTimeComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();

            bool success = _reservationService.TryEditReservation(
                _guest.Id,
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
                DialogResult = true;
                Close();
            }
        }
    }
}