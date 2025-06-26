namespace AppointmentBooking.Application.DTOs.Authentication
{
    public class AuthenticationResultDto
    {
        public bool Success { get; set; }
        public string? Token { get; set; }
        public string? ErrorMessage { get; set; }

        public AuthenticationResultDto(bool success, string? token = null, string? errorMessage = null)
        {
            Success = success;
            Token = token;
            ErrorMessage = errorMessage;
        }
    }
}
