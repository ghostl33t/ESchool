using server.Models.Domain;

namespace server.Models.DTOs.StudentGrades
{
    public class Create
    {
        public long Id { get; set; }
        public server.Models.Domain.User? Student { get; set; }
        public long StudentId { get; set; }
        public int Grade { get; set; }
        public string Description { get; set; }
        public server.Models.Domain.ClassDepartmentSubjectProfessor? ClassDepartmentSubjectProfessor { get; set; }
        public long ClassDepartmentSubjectProfessorId { get; set; }
        public long CreatedById { get; set; }
        
    }
}
