using server.Models.DTOs;
using server.Models.DTOs.ClassDepartment;
using server.Models.DTOs.Subject;
using server.Models.DTOs.UsersDTO;

namespace server.Validations
{
    public interface ICDSP
    {
        public bool validationResult { get; set; }
        public Task<bool> ValidateCreator(UsersDTO CreatedBy);
        public Task<bool> ValidateSubject(SubjectDTO Subject);
        public Task<bool> ValidateProfessor(UsersDTO User);
        public Task<bool> ValidateClassDepartment(ClassDepartmentDTO ClassDepDTO);
        public Task<string> Validate(SubjectDTO subject, UsersDTO prof, UsersDTO creator, ClassDepartmentDTO classdepdto);
    }
}
