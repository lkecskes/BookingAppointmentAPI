namespace AppointmentBooking.Contracts.Authentication
{
    public record RegisterResponse(
        bool IsSuccess,
        string? Token,
        string? Message
    );
}
