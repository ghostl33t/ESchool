namespace server.Repositories.Interfaces
{
    public interface IClassDepartment
    {
        public Task<List<Models.DTOs.ClassDepartment.ClassDepartmentDTO>> GetSchoolsList();
        public Task<Models.DTOs.ClassDepartment.ClassDepartmentDTO> GetSchoolById(long Id);
        public Task<Models.DTOs.ClassDepartment.Create> CreateSchoolAsync(Models.DTOs.ClassDepartment.Create newclassdepp);
        public Task<Models.DTOs.ClassDepartment.ClassDepartmentDTO> ModifySchoolAsync(Models.DTOs.ClassDepartment.Update classdep);
        public Task<Models.DTOs.ClassDepartment.ClassDepartmentDTO> DeleteSchoolAsync(long Id);

        public Task<List<server.Models.DTOs.StudentDetails.StudentDetailsDTO>> GetStudentsPerClassDetilsAsync(long id);
    }
}
