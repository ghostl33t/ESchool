using AutoMapper;
using Microsoft.EntityFrameworkCore;
using server.Database;
using server.Models.DTOs.Subject;
using server.Repositories.Interfaces;

namespace server.Repositories.Classes
{
    public class SubjectRepository : ISubjects
    {
        private readonly DBMain DbMain;
        private readonly IMapper IMapper;
        private readonly DBRegistries DBRegistries;
        public SubjectRepository(DBMain DbMain,DBRegistries DBRegistries, IMapper IMapper)
        {
            this.DbMain = DbMain;
            this.IMapper = IMapper;
            this.DBRegistries = DBRegistries;
        }
        public async Task<Models.DTOs.Subject.Create> CreateSubjectAsync(Models.DTOs.Subject.Create newSubject)
        {
            var classDep = IMapper.Map<Models.Domain.Subject>(newSubject);
            await DBRegistries.Subjects.AddAsync(classDep);
            await DbMain.SaveChangesAsync();
            return newSubject;
        }

        public async Task<SubjectDTO> DeleteSubjectAsync(long Id)
        {
            try
            {
                var subject = await DbMain.ClassDepartments.FirstOrDefaultAsync(s => s.ID == Id);
                var subjectDTO = IMapper.Map<Models.DTOs.Subject.SubjectDTO>(subject);
                if (subject != null)
                {
                    subject.Deleted = 1;
                    subject.DeletedDate = DateTime.Today;
                    await this.DBRegistries.SaveChangesAsync();
                    return subjectDTO;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }

        public async  Task<SubjectDTO> GetSubjectById(long Id)
        {
            try
            {
                var subject = await this.DBRegistries.Subjects.FirstOrDefaultAsync(s => s.Id == Id);
                var subjectDTO = IMapper.Map<SubjectDTO>(subject);
                if(subjectDTO != null)
                {
                    return subjectDTO;
                }
                
            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }

        public async Task<List<SubjectDTO>> GetSubjectsList()
        {
            try
            {
                var subjects = await this.DBRegistries.Subjects.ToListAsync();
                var subjectDTO = IMapper.Map<List<Models.DTOs.Subject.SubjectDTO>>(subjects);
                if(subjectDTO != null)
                {
                    return subjectDTO;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }

        public async Task<SubjectDTO> ModifySubject(Update classdep)
        {
            try
            {
                var subjectExist = await this.DBRegistries.Subjects.FirstOrDefaultAsync(s => s.Id == classdep.ID);
                if(subjectExist != null)
                {
                    subjectExist.SerialNumber = classdep.SerialNumber;
                    subjectExist.Name = classdep.Name;
                    subjectExist.SchoolType = classdep.SchoolType;
                    //var leadprofmap = await DbMain.Users.FirstOrDefaultAsync(s => s.Id == classdep.LeaderProfessorId);
                    //subjectExist.LeaderProfessor = leadprofmap;//classdep.LeaderProfessorId;
                    //subjectExist.Year = classdep.Year;

                    await this.DBRegistries.SaveChangesAsync();
                    var subjectsDTO = IMapper.Map<SubjectDTO>(subjectExist);
                    return subjectsDTO;
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
