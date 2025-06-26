using AppointmentBooking.Domain.Enums;

namespace AppointmentBooking.Contracts.Authentication
{
    public record LoginRequest(
        UserType UserType,
        string FirstName,
        string LastName,
        string UserName,
        string EmailAddress,
        string Password
    );
}
