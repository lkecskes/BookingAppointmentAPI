using AppointmentBooking.Application.DTOs.Authentication;
using AppointmentBooking.Application.Services.Users;
using AppointmentBooking.Domain.Entities;
using AppointmentBooking.Domain.Enums;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace AppointmentBooking.Application.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<AuthenticationResultDto> LoginAsync(AuthenticationLoginParamsDto @params)
        {
            var user = await _userRepository.GetUserByEmailAsync(@params.Email);
            if (user == null)
            {
                return new AuthenticationResultDto(false, null, "Nem található a felhasználó!");
            }

            // Jelszó ellenőrzése BCrypt segítségével
            var validPassword = BCrypt.Net.BCrypt.Verify(@params.Password, user.Password);
            if (!validPassword)
            {
                return new AuthenticationResultDto(false, null, "Érvénytelen jelszó!");
            }

            // Token generálás (pl. JWT vagy ideiglenes token)
            var token = GenerateToken(user);

            return new AuthenticationResultDto(true, token);
        }

        public async Task<AuthenticationResultDto> RegisterAsync(AuthenticationRegisterParamsDto @params)
        {
            // 1. Létező user ellenőrzése
            var existingUser = await _userRepository.GetUserByEmailAsync(@params.Email);
            if (existingUser != null)
            {
                return new AuthenticationResultDto(false, null, "Ezzel az Email címmel már regisztráltak!");
            }

            // User type check

            bool isValid = Enum.IsDefined(typeof(UserType), @params.UserType);

            if (!isValid)
            {
                return new AuthenticationResultDto(false, null, "Érvénytelen felhasználói típus!");
            }

            // Email check

            if (!IsValidEmail(@params.Email))
            {
                return new AuthenticationResultDto(false, null, "Érvénytelen email formátum!");
            }

            // 2. Jelszó hash-elése
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(@params.Password);

            // 3. Új User létrehozása
            var user = new UserEntity
            {
                FirstName = @params.FirstName,
                LastName = @params.LastName,
                EmailAddress = @params.Email,
                UserName = @params.UserName,
                UserType = @params.UserType,
                Password = passwordHash
            };

            // 4. User hozzáadása az adatbázishoz
            await _userRepository.AddUserAsync(user);

            // 5. Mentés az adatbázisba
            await _userRepository.SaveChangesAsync();

            // 6. Token generálás (egyelőre egyszerű)
            var token = GenerateToken(user);

            // 7. Eredmény visszaadása
            return new AuthenticationResultDto(true, token);
        }

        private string GenerateToken(UserEntity user)
        {
            // TODO: JWT vagy más token generálás
            return "fake-jwt-token";
        }

        /// <summary>
        /// Email format checker
        /// </summary>
        /// <param name="email">Email address came from user</param>
        /// <returns>Determines if the email format is valid or not</returns>
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize and validate with MailAddress
                var normalizedEmail = new MailAddress(email).Address;

                // Optional stricter format check with regex
                return Regex.IsMatch(
                    normalizedEmail,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase | RegexOptions.Compiled
                );
            }
            catch
            {
                return false;
            }
        }
    }
}
