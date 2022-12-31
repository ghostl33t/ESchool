using server.Models.Domain;

namespace server.Repositories.Interfaces
{
    public interface IStudentDetails
    {
        public Task<long> CreateStudentDetails(StudentDetails studentdet);
        public Task<long> UpdateStudentDetails( StudentDetails studentdet);
        public Task<long> DeleteStudentDetails(long Id, long AdministratorId);
       
    }
}
