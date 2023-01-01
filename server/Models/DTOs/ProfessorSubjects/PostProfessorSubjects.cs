namespace server.Models.DTOs.ProfessorSubjects
{
    public class PostProfessorSubjects
    {
        public long ProfessorId_ { get; set; }
        public long SubjectId { get; set; }
        public long CreatedById_ { get; set; } = 0;
    }
}
