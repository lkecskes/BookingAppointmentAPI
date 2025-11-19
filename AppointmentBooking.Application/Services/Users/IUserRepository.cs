using AppointmentBooking.Domain.Entities;

namespace AppointmentBooking.Application.Services.Users
{
    public interface IUserRepository
    {
        Task<UserEntity?> GetUserByEmailAsync(string email);
        Task<UserEntity?> GetUserByIdAsync(int userId);
        Task AddUserAsync(UserEntity user);
        Task UpdateUserAsync(UserEntity user);
        Task SaveChangesAsync();
    }
}
