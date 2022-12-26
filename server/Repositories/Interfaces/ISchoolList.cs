using server.Models.Domain;

namespace server.Repositories.Interfaces
{
    public interface ISchoolList
    {
        public Task<List<SchoolList>> GetSchoolsList();
        public Task<SchoolList> GetSchoolById(long Id);
        public Task<long> CreateSchoolAsync(SchoolList newSchool);
        public Task<long> ModifySchoolAsync(SchoolList school);
        public Task<long> DeleteSchoolAsync(long SchoolId, long AdministratorId);
    }
}
