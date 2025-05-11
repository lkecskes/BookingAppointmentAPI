using AppointmentBooking.Application.DTOs.Booking.Dto;

namespace AppointmentBooking.Application.Interfaces
{
    interface IBookingService
    {
        Task CreateBookingAsync(CreateBookingDto dto);
    }
}
