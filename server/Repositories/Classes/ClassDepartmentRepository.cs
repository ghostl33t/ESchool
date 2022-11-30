using AutoMapper;
using Microsoft.EntityFrameworkCore;
using server.Database;
using server.Models.DTOs.ClassDepartment;
using server.Repositories.Interfaces;

namespace server.Repositories.Classes
{
    public class ClassDepartmentRepository : IClassDepartment
    {
        private readonly DBMain DbMain;
        private readonly IMapper IMapper;
        public ClassDepartmentRepository(DBMain DbMain, IMapper IMapper)
        {
            this.DbMain = DbMain;
            this.IMapper = IMapper;
        }
        public async Task<Models.DTOs.ClassDepartment.Create> CreateSchoolAsync(Models.DTOs.ClassDepartment.Create newclassdepp)
        {
            var classDep = IMapper.Map<Models.Domain.ClassDepartment>(newclassdepp);
            await DbMain.ClassDepartments.AddAsync(classDep);
            await DbMain.SaveChangesAsync();
            return newclassdepp;
        }

        public async Task<ClassDepartmentDTO> DeleteSchoolAsync(long Id)
        {
            try
            {
                var classdep = await DbMain.ClassDepartments.FirstOrDefaultAsync(s => s.ID == Id);
                var classdepDTO = IMapper.Map<Models.DTOs.ClassDepartment.ClassDepartmentDTO>(classdep);
                if (classdep != null)
                {
                    classdep.Deleted = 1;
                    classdep.DeletedDate = DateTime.Today;
                    await DbMain.SaveChangesAsync();
                    return classdepDTO;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }

        public async  Task<ClassDepartmentDTO> GetSchoolById(long Id)
        {
            try
            {
                var classDep = await DbMain.ClassDepartments.FirstOrDefaultAsync(s => s.ID == Id);
                var classDepDTO = IMapper.Map<ClassDepartmentDTO>(classDep);
                if(classDepDTO != null)
                {
                    return  classDepDTO;
                }
                
            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }

        public async Task<List<ClassDepartmentDTO>> GetSchoolsList()
        {
            try
            {
                var classDeps = await DbMain.ClassDepartments.ToListAsync();
                var classDepsDTO = IMapper.Map<List<Models.DTOs.ClassDepartment.ClassDepartmentDTO>>(classDeps);
                if(classDepsDTO != null)
                {
                    return classDepsDTO;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }

        public async Task<ClassDepartmentDTO> ModifySchoolAsync(Update classdep)
        {
            try
            {
                var classDepExist = await DbMain.ClassDepartments.FirstOrDefaultAsync(s => s.ID == classdep.ID);
                if(classDepExist != null)
                {
                    classDepExist.SerialNumber = classdep.SerialNumber;
                    classDepExist.Name = classdep.Name;
                    classDepExist.SchoolListId = classdep.SchoolListId;
                    var leadprofmap = await DbMain.Users.FirstOrDefaultAsync(s => s.Id == classdep.LeaderProfessorId);
                    classDepExist.LeaderProfessor = leadprofmap;//classdep.LeaderProfessorId;
                    classDepExist.Year = classdep.Year;

                    await DbMain.SaveChangesAsync();
                    var classDepDTO = IMapper.Map<ClassDepartmentDTO>(classDepExist);
                    return classDepDTO;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return null;
        }
    }
}
