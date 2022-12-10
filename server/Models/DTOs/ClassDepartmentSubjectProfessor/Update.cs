using server.Models.Domain;

namespace server.Models.DTOs.ClassDepartmentSubjectProfessor
{
    public class Update
    {
        public long Id { get; set; }
        public long SubjectID { get; set; }
        public server.Models.Domain.ClassDepartment? ClassDepartment { get; set; }
        public User? ClassDepartmentSubjectProfessor { get; set; }
    }
}
