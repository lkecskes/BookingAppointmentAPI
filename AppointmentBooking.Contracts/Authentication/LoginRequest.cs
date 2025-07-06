namespace AppointmentBooking.Contracts.Authentication
{
    public record LoginRequest(
        string EmailAddress,
        string Password
    );
}
