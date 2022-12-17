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
            try
            {
                var subject = IMapper.Map<Models.Domain.Subject>(newSubject);
                await DBRegistries.Subjects.AddAsync(subject);
                await DBRegistries.SaveChangesAsync();
                return newSubject;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<SubjectDTO> DeleteSubjectAsync(long SubjectId, long AdministratorId)
        {
            try
            {
                var subject = await this.DBRegistries.Subjects.FirstOrDefaultAsync(s => s.Id == SubjectId);
                subject.Deleted = 1;
                subject.DeletedDate = DateTime.Today;
                subject.DeletedById = AdministratorId;
                await this.DBRegistries.SaveChangesAsync();
                var subjectDTO = IMapper.Map<SubjectDTO>(subject);
                return subjectDTO;
            }
            catch (Exception)
            {
                throw;
            }
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
                var subjects = await this.DBRegistries.Subjects.Where(s=>s.Deleted == 0).ToListAsync();
                var subjectDTO = IMapper.Map<List<Models.DTOs.Subject.SubjectDTO>>(subjects);
                if (subjectDTO != null)
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
        public async Task<SubjectDTO> ModifySubject(Update subject)
        {
            try
            {
                var subjectExist = await this.DBRegistries.Subjects.FirstOrDefaultAsync(s => s.Id == subject.Id);
                if(subjectExist != null)
                {
                    subjectExist.SerialNumber = subject.SerialNumber;
                    subjectExist.Name = subject.Name;
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
