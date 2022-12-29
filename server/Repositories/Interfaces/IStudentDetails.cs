using server.Models.Domain;

namespace server.Repositories.Interfaces
{
    public interface IStudentDetails
    {
        public Task<bool> CreateStudentDetails(StudentDetails studentdet);
       
    }
}
