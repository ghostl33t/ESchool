using server.Models.Domain;
using server.Models.DTOs.StudentDetails;

namespace server.Repositories.Interfaces
{
    public interface IClassDepartment
    {
        public Task<List<ClassDepartment>> GetAllClassDepartmentsAsync();
        public Task<ClassDepartment> GetClassDepartmentByIdAsync(long Id);
        public Task<long> CreateClassDepartmentAsync(ClassDepartment newclassdepp);
        public Task<ClassDepartment> ModifyClassDepartmentAsync(ClassDepartment updatedclassdep);
        public Task<long> DeleteClassDepartmentAsync(long Id, long AdministratorId);

        public Task<List<GetStudentDetails>> GetStudentsPerClassDetailsAsync(long id);
    }
}
