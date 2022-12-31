namespace server.Models.DTOs.ProfessorSubjects
{
    public class PatchProfessorSubjects
    {
        public long ProfessorId_ { get; set; }
        public long SubjectId_ { get; set; }
        public long UpdatedById { get; set; }
    }
}
