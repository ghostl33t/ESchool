using server.Models.DTOs.StudentDetails;

namespace server.Validations.Interfaces;
public interface IStudentDetailsValidations
{
    int code { get; set; }
    string validationMessage { get; set; }
    public Task<bool> ValidateCreator(long creatorId);
    public Task<bool> ValidateStudent(long studId);
    public Task<bool> ValidateClassDepartment(long cdId);
    public Task<bool> ValidateParent(long parentId);
    public Task<bool> ValidateDiscipline(int? discipline);
    public Task<bool> Validate(PostStudentDetails studentDetails);
    public Task<bool> Validate(long studentDetailsId, PatchStudentDetails studentDetails);
    public Task<bool> Validate(long studentDetailsId, long administratorId);
}
