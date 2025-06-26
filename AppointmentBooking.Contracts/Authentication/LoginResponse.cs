namespace AppointmentBooking.Contracts.Authentication
{
    public record LoginResponse(
        bool IsSuccess,
        string? Token,
        string? Message
    );
}
