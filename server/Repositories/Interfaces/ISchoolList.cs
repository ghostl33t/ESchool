namespace server.Repositories.Interfaces
{
    public interface ISchoolList
    {
        public Task<List<Models.DTOs.SchoolList.SchoolList>> GetSchoolsList();
        public Task<Models.DTOs.SchoolList.SchoolList> GetSchoolById(long Id);
        public Task<Models.DTOs.SchoolList.SchoolList> CreateSchoolAsync(Models.DTOs.SchoolList.Create newSchool);
        public Task<Models.DTOs.SchoolList.SchoolList> ModifySchoolAsync(Models.DTOs.SchoolList.Update school);
        public Task<Models.DTOs.SchoolList.SchoolList> DeleteSchoolAsync(long Id);
    }
}
