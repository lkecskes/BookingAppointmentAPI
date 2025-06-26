using AppointmentBooking.Domain.Enums;

namespace AppointmentBooking.Domain.Entities
{
    public class UserEntity
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public UserType UserType { get; set; }
        public string Password { get; set; } = string.Empty;
    }
}
