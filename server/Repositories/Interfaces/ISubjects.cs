using AutoMapper;
using server.Database;
using server.Models.DTOs.Subject;

namespace server.Repositories.Interfaces
{
    public interface ISubjects
    {
        public Task<Models.DTOs.Subject.Create> CreateSubjectAsync(Models.DTOs.Subject.Create newSubject);
        public Task<Models.DTOs.Subject.SubjectDTO> DeleteSubjectAsync(long SubjectId, long AdministratorId);
        public Task<SubjectDTO> GetSubjectById(long Id);
        public Task<List<SubjectDTO>> GetSubjectsList();
        public Task<SubjectDTO> ModifySubject(Update classdep);
    }
}
