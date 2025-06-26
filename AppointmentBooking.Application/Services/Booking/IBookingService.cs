using AppointmentBooking.Application.DTOs.Booking.Dto;

namespace AppointmentBooking.Application.Services.Booking
{
    public interface IBookingService
    {
        Task CreateBookingAsync(CreateBookingDto dto);
    }
}
