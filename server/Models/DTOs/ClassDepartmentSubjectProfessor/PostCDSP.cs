
namespace server.Models.DTOs.ClassDepartmentSubjectProfessor;

public class PostCDSP
{
    public long Id { get; set; }
    public long SubjectID { get; set; }
    public long ClassDepartmentId { get; set; }
    public long ProfessorId { get; set; }
    public long CreatedById { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public int Deleted { get; set; } = 0;
    public DateTime? DeletedDate { get; set; }
    public long DeletedById { get; set; }

}
