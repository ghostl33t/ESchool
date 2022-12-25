using Microsoft.EntityFrameworkCore;
using server.Database;
using server.Models.Domain;

namespace server.Services.DataService
{
    public class DataService : IDataService
    {
        public static List<ClassDepartment> tmpClassDepartment = new List<ClassDepartment>();
        private readonly DBMain _dbMain;
        public DataService(DBMain dbMain)
        {
            _dbMain = dbMain;
        }
        public async Task<bool> FillData()
        {
            tmpClassDepartment = await _dbMain.ClassDepartments.Where(s => s.Deleted == 1).ToListAsync();
            return await Task.FromResult(true);
        }
    }
}
