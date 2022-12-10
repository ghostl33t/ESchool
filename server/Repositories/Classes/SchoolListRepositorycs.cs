using AutoMapper;
using Microsoft.EntityFrameworkCore;
using server.Database;
using server.Repositories.Interfaces;

namespace server.Repositories.Classes
{
    public class SchoolListRepositorycs : ISchoolList
    {
        private readonly DBRegistries DBRegistries;
        private readonly DBMain DbMain;
        private readonly Validations.ISchoolListValidations ISchoolListValidations;
        private readonly IMapper Mapper;
        public SchoolListRepositorycs(DBRegistries DBRegistries, Validations.ISchoolListValidations iSchoolListValidations, IMapper mapper,DBMain DbMain)
        {
            this.DbMain = DbMain;
            this.DBRegistries = DBRegistries;
            this.ISchoolListValidations = iSchoolListValidations;
            this.Mapper = mapper;
        }
        public async Task<List<Models.DTOs.SchoolList.ClassDepartmentSubjectProfessorDTO>> GetSchoolsList()
        {
            try
            {
                var schoolList = await DBRegistries.SchoolList.Where(s => s.Deleted == 0).ToListAsync();
                var schoolListDTO = Mapper.Map<List<Models.DTOs.SchoolList.ClassDepartmentSubjectProfessorDTO>>(schoolList);
                return schoolListDTO;
            }
            catch (Exception)
            {

                throw;
            }
            
            
        }
        public async Task<Models.DTOs.SchoolList.ClassDepartmentSubjectProfessorDTO> GetSchoolById(long Id)
        {
            try
            {
                var school = await DBRegistries.SchoolList.FirstOrDefaultAsync(s => s.Id == Id);
                var schoolDTO = Mapper.Map<Models.DTOs.SchoolList.ClassDepartmentSubjectProfessorDTO>(school);
                return schoolDTO;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<Models.DTOs.SchoolList.ClassDepartmentSubjectProfessorDTO> CreateSchoolAsync(Models.DTOs.SchoolList.Create newSchool)
        {
            try
            {
                var school = Mapper.Map<Models.Domain.SchoolList>(newSchool);
                await DBRegistries.SchoolList.AddAsync(school);
                await DBRegistries.SaveChangesAsync();
                var schoolForReturn = Mapper.Map<Models.DTOs.SchoolList.ClassDepartmentSubjectProfessorDTO>(school);
                return schoolForReturn;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<Models.DTOs.SchoolList.ClassDepartmentSubjectProfessorDTO> ModifySchoolAsync(Models.DTOs.SchoolList.Update schoolDTO)
        {
            try
            {
                var school = await DBRegistries.SchoolList.FirstOrDefaultAsync(s => s.Id == schoolDTO.Id);
                    school.SerialNumber = schoolDTO.SerialNumber;
                    school.Name = schoolDTO.Name;
                    school.SchoolType = schoolDTO.SchoolType;
                await DBRegistries.SaveChangesAsync();
                return Mapper.Map<Models.DTOs.SchoolList.ClassDepartmentSubjectProfessorDTO>(school);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<Models.DTOs.SchoolList.ClassDepartmentSubjectProfessorDTO> DeleteSchoolAsync(long Id)
        {
            try
            {
                var school = await DBRegistries.SchoolList.FirstOrDefaultAsync(s => s.Id == Id);
                school.Deleted = 1;
                school.DeletedDate = DateTime.Today;
                await DBRegistries.SaveChangesAsync();
                return Mapper.Map<Models.DTOs.SchoolList.ClassDepartmentSubjectProfessorDTO>(school);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
