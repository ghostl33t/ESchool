using AutoMapper;
using Microsoft.EntityFrameworkCore;
using server.Database;
using server.Models.Domain;
using server.Repositories.Interfaces;

namespace server.Repositories.Classes
{
    public class SchoolListRepositorycs : ISchoolList
    {
        private readonly DBMain _dbMain;
        private readonly IMapper _mapper;
        public SchoolListRepositorycs(IMapper mapper,DBMain dbMain)
        {
            this._dbMain = dbMain;
            this._mapper = mapper;
        }
        public async Task<List<SchoolList>> GetSchoolsList()
        {
            try
            {
                var schoolList = await _dbMain.SchoolList.Where(s => s.Deleted == 0).ToListAsync();
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
                var school = await _dbMain.SchoolList.FirstOrDefaultAsync(s => s.Id == Id);
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
                await _dbMain.SchoolList.AddAsync(newSchool);
                await _dbMain.SaveChangesAsync();
                return newSchool.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<long> ModifySchoolAsync(long Id, SchoolList school)
        {
            try
            {
                school.Id = Id;
                _dbMain.SchoolList.Update(school);
                await _dbMain.SaveChangesAsync();
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
                var school = await _dbMain.SchoolList.FirstOrDefaultAsync(s => s.Id == SchoolId);
                school.Deleted = 1;
                school.DeletedById = AdministratorId;
                school.DeletedDate = DateTime.Today;
                await _dbMain.SaveChangesAsync();
                return school.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
