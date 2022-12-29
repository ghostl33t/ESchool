using server.Models.Domain;
using server.Models.DTOs.ClassDepartmentSubjectProfessor;

namespace server.Repositories.Interfaces
{
    public interface ICDSP
    {
        public Task<long> CreateCDSP(ClassDepartmentSubjectProfessor newcdsp);
        public Task<long> ModifyCDSP(long Id, ClassDepartmentSubjectProfessor newcdsp);
        public Task<long> DeleteCDSP(long cdspId, long administratorId);
        public Task<List<GetClassDetails>> GetClassDetails(long classdepid);
        public Task<List<GetProfesorSubjectDetails>> GetProfessorSubjectDetails(long professorId);

    }
}
