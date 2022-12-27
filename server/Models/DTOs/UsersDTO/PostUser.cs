using server.Models.Domain;

namespace server.Models.DTOs.UsersDTO
{
    public class PostUser
    {
        public string UserName { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public string Name { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public int UserType { get; set; } 
        public DateTime DateOfBirth { get; set; } 
        public string OIB { get; set; } = String.Empty;
        public string Phone { get; set; } = String.Empty;
        public long CreatedById { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Email { get; set; } = String.Empty;
    }
}
