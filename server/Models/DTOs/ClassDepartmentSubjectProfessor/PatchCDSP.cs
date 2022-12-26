
namespace server.Models.DTOs.ClassDepartmentSubjectProfessor
{
    public class PatchCDSP
    {
        public long Id { get; set; }
        public long SubjectId { get; set; }
        public long ClassDepartmentId { get; set; }
        public long ClassDepartmentSubjectProfessorId { get; set; }
    }
}
