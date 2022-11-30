using server.Models.Domain;

namespace server.Models.DTOs.UsersDTO
{
    public class Create
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public int UserType { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? OIB { get; set; }
        public string? Phone { get; set; }
        public long CreatedById { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
