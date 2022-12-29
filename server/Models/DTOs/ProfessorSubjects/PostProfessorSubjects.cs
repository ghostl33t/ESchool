namespace server.Models.DTOs.ProfessorSubjects
{
    public class PostProfessorSubjects
    {
        public long ProfessorId_ { get; set; }
        public long SubjectId_ { get; set; }
        public long CreatedById { get; set; } = 0;
    }
}
