using AppointmentBooking.Application.DTOs.Authentication;
using AppointmentBooking.Contracts.Authentication;

namespace AppointmentBooking.Api.Mappers
{
    public static class AuthenticationMapper
    {
        public static AuthenticationRegisterParamsDto ToDto(this RegisterRequest request)
        {
            return new AuthenticationRegisterParamsDto(
                request.FirstName,
                request.LastName,
                request.EmailAddress,
                request.UserType,
                request.UserName,
                request.Password
            );
        }

        public static AuthenticationLoginParamsDto ToDto(this LoginRequest request)
        {
            return new AuthenticationLoginParamsDto(
                request.EmailAddress,
                request.Password
            );
        }
    }
}