using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models.DTOs.ClassDepartment
{
    public class ClassDepartmentDTO
    {
        public long ID { get; set; }
        public string SerialNumber { get; set; }
        public string Name { get; set; }
        public long SchoolListId { get; set; }

        public int Year { get; set; }
        public long LeaderProfessorId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public long CreatedById { get; set; }

        public long DeletedById { get; set; }
        public DateTime? DeletedDate { get; set; } = DateTime.Now;
        public int? Deleted { get; set; }

    }
}
