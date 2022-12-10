using server.Models.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models.DTOs.ClassDepartmentSubjectProfessor
{
    public class ClassDepartmentSubjectProfessorDTO
    {
        public long Id { get; set; }
        public long SubjectID { get; set; }
        public server.Models.Domain.ClassDepartment? ClassDepartment { get; set; }
        public User? ClassDepartmentSubjectProfessor { get; set; }
        public User? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int Deleted { get; set; } = 0;
        public DateTime? DeletedDate { get; set; }
        public long DeletedById { get; set; }

    }
}
