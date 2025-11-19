using AppointmentBooking.Application.DTOs.Authentication;
using AppointmentBooking.Application.Services.Users;
using AppointmentBooking.Domain.Entities;
using AppointmentBooking.Domain.Enums;

namespace AppointmentBooking.Application.Services.Authentication
{
    public class AuthService(IUserRepository userRepository, IJwtService jwtService) : IAuthService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IJwtService _jwtService = jwtService;

        public async Task<AuthenticationResultDto> LoginAsync(AuthenticationLoginParamsDto @params)
        {
            var user = await _userRepository.GetUserByEmailAsync(@params.Email);
            if (user == null)
            {
                return new AuthenticationResultDto(false, null, "Nem található a felhasználó!");
            }

            var validPassword = BCrypt.Net.BCrypt.Verify(@params.Password, user.Password);
            if (!validPassword)
            {
                return new AuthenticationResultDto(false, null, "Érvénytelen jelszó!");
            }

            var token = _jwtService.GenerateToken(user);
            return new AuthenticationResultDto(true, token, "Sikeres belépés!");
        }

        public async Task<AuthenticationResultDto> RegisterAsync(AuthenticationRegisterParamsDto @params)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(@params.Email);
            if (existingUser != null)
            {
                return new AuthenticationResultDto(false, null, "Ezzel az Email címmel már regisztráltak!");
            }

            bool isValid = Enum.IsDefined(typeof(UserType), @params.UserType);
            if (!isValid)
            {
                return new AuthenticationResultDto(false, null, "Érvénytelen felhasználói típus!");
            }

            if (!IsValidEmail(@params.Email))
            {
                return new AuthenticationResultDto(false, null, "Érvénytelen email formátum!");
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(@params.Password);

            var user = new UserEntity
            {
                FirstName = @params.FirstName,
                LastName = @params.LastName,
                EmailAddress = @params.Email,
                UserName = @params.UserName,
                UserType = @params.UserType,
                Password = passwordHash
            };

            await _userRepository.AddUserAsync(user);
            await _userRepository.SaveChangesAsync();

            var token = _jwtService.GenerateToken(user);
            return new AuthenticationResultDto(true, token);
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
