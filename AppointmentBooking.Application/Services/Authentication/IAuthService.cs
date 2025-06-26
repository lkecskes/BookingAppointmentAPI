using AppointmentBooking.Application.DTOs.Authentication;

namespace AppointmentBooking.Application.Services.Authentication
{
    public interface IAuthService
    {
        Task<AuthenticationResultDto> LoginAsync(AuthenticationLoginParamsDto @params);
        Task<AuthenticationResultDto> RegisterAsync(AuthenticationRegisterParamsDto @params);
    }
}
