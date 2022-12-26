using server.Models.DTOs;
using server.Models.DTOs.ClassDepartment;
using server.Models.DTOs.Subject;
using server.Models.DTOs.UsersDTO;

namespace server.Validations.Interfaces
{
    public interface ICDSPValidations
    {
        public string validationMessage { get; set; }
        public int code { get; set; }
        public Task<bool> ValidateCreator(long createdById);
        public Task<bool> ValidateSubject(long subjectId);
        public Task<bool> ValidateProfessor(long professorId);
        public Task<bool> ValidateClassDepartment(long classDepId);
        public Task<bool> Validate(long creatorId, long subjectId, long professorId, long classDepId);
        public Task<bool> Validate(long Id, long administratorId);
    }
}
