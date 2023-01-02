namespace server.Models.DTOs.StudentGrades
{
    public class PatchStudentGrades
    {
        public long StudentId_ { get; set; }
        public long ProfessorId_ { get; set; }
        public long SubjectId_ { get; set; }
        public int Grade { get; set; }
        public string Description { get; set; } = String.Empty;

        public DateTime GradeDate { get; set; } = DateTime.Now;
    }
}
