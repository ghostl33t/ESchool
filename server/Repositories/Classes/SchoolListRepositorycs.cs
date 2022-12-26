using AutoMapper;
using Microsoft.EntityFrameworkCore;
using server.Database;
using server.Models.Domain;
using server.Repositories.Interfaces;

namespace server.Repositories.Classes
{
    public class SchoolListRepositorycs : ISchoolList
    {
        private readonly DBRegistries _dbRegistries;
        private readonly DBMain _dbMain;
        private readonly IMapper _mapper;
        public SchoolListRepositorycs(DBRegistries dbRegistries, IMapper mapper,DBMain dbMain)
        {
            this._dbMain = dbMain;
            this._dbRegistries = dbRegistries;
            this._mapper = mapper;
        }
        public async Task<List<SchoolList>> GetSchoolsList()
        {
            try
            {
                var schoolList = await _dbRegistries.SchoolList.Where(s => s.Deleted == 0).ToListAsync();
                return schoolList;
            }
            catch (Exception)
            {

                throw;
            }
            
            
        }
        public async Task<SchoolList> GetSchoolById(long Id)
        {
            try
            {
                var school = await _dbRegistries.SchoolList.FirstOrDefaultAsync(s => s.Id == Id);
                return school;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<long> CreateSchoolAsync(SchoolList newSchool)
        {
            try
            {
                newSchool.CreatedDate = DateTime.Today;
                await _dbRegistries.SchoolList.AddAsync(newSchool);
                await _dbRegistries.SaveChangesAsync();
                return newSchool.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<long> ModifySchoolAsync(SchoolList school)
        {
            try
            {
                _dbRegistries.SchoolList.Update(school);
                await _dbRegistries.SaveChangesAsync();
                return school.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<long> DeleteSchoolAsync(long SchoolId, long AdministratorId)
        {
            try
            {
                var school = await _dbRegistries.SchoolList.FirstOrDefaultAsync(s => s.Id == SchoolId);
                school.Deleted = 1;
                school.DeletedById = AdministratorId;
                school.DeletedDate = DateTime.Today;
                await _dbRegistries.SaveChangesAsync();
                return school.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
