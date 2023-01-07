namespace server.Models.DTOs.StudentGrades
{
    public class GetStudentGrades
    {
        public string SubjectName { get; set; } = string.Empty;
        public DateTime GradesDate { get; set; } = new();
        public string GradeDescription { get; set; } = string.Empty;
        public int Grade { get; set; }
        public string ProfessorNameAndSurname { get; set; } = string.Empty;

    }
}
