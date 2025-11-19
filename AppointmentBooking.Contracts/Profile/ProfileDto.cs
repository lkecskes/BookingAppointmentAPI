using AppointmentBooking.Domain.Enums;

namespace AppointmentBooking.Contracts.Profile
{
    public record ProfileResponse(
        int UserId,
        string UserName,
        string EmailAddress,
        string FirstName,
        string LastName,
        UserType UserType
    );

    public record UpdateProfileRequest(
        string UserName,
        string EmailAddress,
        string FirstName,
        string LastName,
        string? Password // csak akkor frissül, ha meg van adva
    );
}
