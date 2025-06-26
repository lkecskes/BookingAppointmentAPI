using AppointmentBooking.Domain.Enums;

namespace AppointmentBooking.Contracts.Authentication
{
    public record AuthenticationResponse(
    Guid UserId,
    UserType UserType,
    string FirstName,
    string LastName,
    string UserName,
    string EmailAddress
);
}
