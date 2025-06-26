using AppointmentBooking.Application.Services.Users;
using AppointmentBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBooking.Infrastructure.Persistance.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserEntity?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.EmailAddress == email);
        }

        public async Task AddUserAsync(UserEntity user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
