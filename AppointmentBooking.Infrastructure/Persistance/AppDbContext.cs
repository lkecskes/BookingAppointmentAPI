using AppointmentBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBooking.Infrastructure.Persistance.Repositories
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<BookingEntity> Bookings { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        // Ide jön a többi entity is majd:
        // public DbSet<User> Users { get; set; }
        // public DbSet<Service> Services { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Itt lehet finomhangolni, pl. tábla neveket, kulcsokat, indexeket stb.
            modelBuilder.Entity<UserEntity>().ToTable("User");
            modelBuilder.Entity<BookingEntity>().ToTable("Bookings");

            // explicit primary key beállítás
            modelBuilder.Entity<UserEntity>()
                .HasKey(u => u.UserId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
