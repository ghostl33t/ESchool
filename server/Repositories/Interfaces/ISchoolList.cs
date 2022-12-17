namespace server.Repositories.Interfaces
{
    public interface ISchoolList
    {
        public Task<List<Models.DTOs.SchoolList.ClassDepartmentSubjectProfessorDTO>> GetSchoolsList();
        public Task<Models.DTOs.SchoolList.ClassDepartmentSubjectProfessorDTO> GetSchoolById(long Id);
        public Task<Models.DTOs.SchoolList.ClassDepartmentSubjectProfessorDTO> CreateSchoolAsync(Models.DTOs.SchoolList.Create newSchool);
        public Task<Models.DTOs.SchoolList.ClassDepartmentSubjectProfessorDTO> ModifySchoolAsync(Models.DTOs.SchoolList.Update school);
        public Task<Models.DTOs.SchoolList.ClassDepartmentSubjectProfessorDTO> DeleteSchoolAsync(long SchoolId, long AdministratorId);
    }
}
