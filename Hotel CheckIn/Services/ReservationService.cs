using Hotel_CheckIn.Models;
using System;
using System.Collections.Generic;

namespace Hotel_CheckIn.Services
{
    public class ReservationService
    {
        private readonly GuestRepository _repository;
        private readonly GuestValidationService _validationService;
        private readonly EmailService _emailService;

        public ReservationService()
        {
            _repository = new GuestRepository();
            _validationService = new GuestValidationService();
            _emailService = new EmailService();
        }

        public bool TryCheckInGuest(
            string fullName,
            string idPassport,
            string roomNumber,
            string email,
            DateTime? checkInDate,
            DateTime? checkOutDate,
            string? checkInTime,
            string? checkOutTime,
            out string message)
        {
            if (!_validationService.ValidateGuestInput(
                fullName,
                idPassport,
                roomNumber,
                email,
                checkInDate,
                checkOutDate,
                checkInTime,
                checkOutTime,
                out message))
            {
                return false;
            }

            var guest = new Guest
            {
                FullName = fullName,
                IdPassport = idPassport,
                RoomNumber = roomNumber,
                Email = email,
                CheckInDate = _validationService.CombineDateAndTime(checkInDate!.Value, checkInTime!),
                CheckOutDate = _validationService.CombineDateAndTime(checkOutDate!.Value, checkOutTime!),
                IsCheckedOut = false
            };

            _repository.Add(guest);
            _emailService.OpenCheckInEmail(guest.Email, guest.FullName, guest.RoomNumber);

            message = "Guest saved successfully.";
            return true;
        }

        public List<Guest> GetCurrentReservations()
        {
            return _repository.GetCurrentReservations();
        }

        public List<Guest> GetHistory()
        {
            return _repository.GetHistory();
        }

        public bool CheckOutGuest(int guestId)
        {
            var guest = _repository.GetById(guestId);
            if (guest == null)
                return false;

            guest.IsCheckedOut = true;
            guest.CheckOutDate = DateTime.Now;
            _repository.Update(guest);

            _emailService.OpenCheckOutEmail(guest.Email, guest.FullName, guest.RoomNumber);
            return true;
        }
    }
}