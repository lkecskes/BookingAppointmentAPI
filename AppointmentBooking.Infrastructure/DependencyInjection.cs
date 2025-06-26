using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AppointmentBooking.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Itt adhatsz hozzá infrastruktúra szintű szolgáltatásokat, pl. fájlkezelés, e-mail, stb.

            return services;
        }
    }
}
