using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models.DTOs.UsersDTO
{
    public class UsersDTO
    {
        public long Id { get; set; }
        public string? Password { get; set; }
        public string? UserName { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public int UserType { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? OIB { get; set; }
        public string? Phone { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? DeletedDate { get; set; } = DateTime.Now;
        public int? Deleted { get; set; }
    }
}
