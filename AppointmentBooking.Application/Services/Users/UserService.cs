using AppointmentBooking.Contracts.Profile;

namespace AppointmentBooking.Application.Services.Users
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ProfileResponse?> GetProfileAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
                return null;

            return new ProfileResponse(
                user.UserId,
                user.UserName,
                user.EmailAddress,
                user.FirstName,
                user.LastName,
                user.UserType
            );
        }

        public async Task<bool> UpdateProfileAsync(int userId, UpdateProfileRequest request)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) return false;

            user.UserName = request.UserName;
            user.EmailAddress = request.EmailAddress;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            }
            await _userRepository.UpdateUserAsync(user);
            await _userRepository.SaveChangesAsync();
            return true;
        }
    }
}
