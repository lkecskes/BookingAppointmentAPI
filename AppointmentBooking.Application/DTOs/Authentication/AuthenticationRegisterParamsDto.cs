using AppointmentBooking.Domain.Enums;

namespace AppointmentBooking.Application.DTOs.Authentication
{
    public record AuthenticationRegisterParamsDto
    (
        string FirstName,
        string LastName,
        string Email,
        UserType UserType,
        string UserName,
        string Password
    );
}
