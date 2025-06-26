using AppointmentBooking.Application.Services.Booking;
using AppointmentBooking.Application.Services.Users;
using AppointmentBooking.Infrastructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AppointmentBooking.Infrastructure.Persistance
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            // 1. AppDbContext regisztrálása (pl. SQL Server-rel)
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // 2. Repositoryk regisztrálása
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            // Add more repositories as needed

            return services;
        }
    }
}
