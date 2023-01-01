using server.Models.Domain;
using server.Models.DTOs.ClassSubjects;

namespace server.Repositories.Interfaces
{
    public interface IClassSubjects
    {
        public Task<List<GetClassSubject>> GetSubjectsPerClass(long classDepartmentId);
        public Task<long> CreateClassSubjects(ClassSubjects classSubject);
        public Task<long> UpdateClassSubjects(long Id, ClassSubjects classSubjects);
        public Task<long> DeleteClassSubjects(long classSubjectId, long leaderId);
    }
}
