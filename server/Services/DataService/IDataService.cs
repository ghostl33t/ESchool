using server.Models.Domain;

namespace server.Services.DataService
{
    public interface IDataService
    {
        public static List<ClassDepartment> tmpClassDepartment = new List<ClassDepartment>();
        public Task<bool> FillData();
    }
}
