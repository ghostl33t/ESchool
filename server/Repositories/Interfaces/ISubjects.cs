using server.Models.Domain;
namespace server.Repositories.Interfaces;

public interface ISubjects
{
    public Task<Subject> GetSubjectById(long Id);
    public Task<List<Subject>> GetSubjectsList();
    public Task<long> CreateSubjectAsync(Subject  newSubject);
    public Task<long> ModifySubject(Subject classdep);
    public Task<long> DeleteSubjectAsync(long SubjectId, long AdministratorId);
    
    
}
