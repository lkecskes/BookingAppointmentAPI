using AppointmentBooking.Domain.Entities;

namespace AppointmentBooking.Application.Services.Users
{
    public interface IUserRepository
    {
        Task<UserEntity?> GetUserByEmailAsync(string email);
        Task AddUserAsync(UserEntity user);
        Task SaveChangesAsync();
    }
}
