using server.Models.DTOs.ClassProfessors;

namespace server.Validations.Interfaces;

public interface IClassProfessorsValidations
{
    public string validationMessage { get; set; }
    public int code { get; set; }

    public Task<bool> ValidateCreator(long leaderId, long classDepId); 
    public Task<bool> ValidateProfessor(long Id); 
    public Task<bool> ValidateClassDepartment(long Id); 

    public Task<bool> Validate(PostClassProfessors classSubject);
    public Task<bool> Validate(long Id, PatchClassProfessors classSubject);
    public Task<bool> Validate(long Id, long leaderId);
}
