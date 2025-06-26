using AppointmentBooking.Application.Services.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace AppointmentBooking.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Automapper, MediatR, service regisztrációk
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }
    }
}
