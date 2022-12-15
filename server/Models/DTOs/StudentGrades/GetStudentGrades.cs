namespace server.Models.DTOs.StudentGrades
{
    public class GetStudentGrades
    {
        public string? StudentNameAndSurname { get; set; }
        public string Subject { get; set; }
        public DateTime GradeDate { get; set; }
        public int Grade { get; set; }
        public string Description { get; set; }
        public string ProfessorNameAndSurname { get; set; }
        public string Verified { get; set; }
    }
}
