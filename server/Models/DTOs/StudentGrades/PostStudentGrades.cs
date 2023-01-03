namespace server.Models.DTOs.StudentGrades
{
    public class PostStudentGrades
    {
        public long StudentId { get; set; }
        public long ProfessorId { get; set; }
        public long SubjectId { get; set; }
        public int Grade { get; set; }
        public string Description { get; set; } = String.Empty;

        public DateTime GradeDate { get; set; } = DateTime.Now;
    }
}
