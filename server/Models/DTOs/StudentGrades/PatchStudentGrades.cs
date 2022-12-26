namespace server.Models.DTOs.StudentGrades
{
    public class PatchStudentGrades
    {
        public server.Models.Domain.User UserStudent { get; set; }
        public int Grade { get; set; }
        public string Description { get; set; }
        public int Validated { get; set; }
        public server.Models.Domain.ClassDepartmentSubjectProfessor? ClassDepartmentSubjectProfessor { get; set; }
    }
}
