using AppointmentBooking.Domain.Enums;

namespace AppointmentBooking.Application.DTOs.User
{
    public class GetAllUserResultDto
    {
        public int UserId { get; set; }
        public UserType UserType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string PassWord { get; set; }

        public GetAllUserResultDto()
        {
            //UserId = result.UserId...
        }
    }
}
