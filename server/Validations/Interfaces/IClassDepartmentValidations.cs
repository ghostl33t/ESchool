using server.Models.DTOs.ClassDepartment;

namespace server.Validations.Interfaces;
public interface IClassDepartmentValidations
{
    public string validationMessage { get; set; }
    public int code { get; set; }
    public Task<bool> ValidateCreator(long CreatedById);
    public Task<bool> ValidateSerialUnique(string serialNumber);
    public Task<bool> ValidateClassSerialNumber(string serialNumber);
    public Task<bool> ValidateClassName(string name);
    public Task<bool> ValidateSchoolListId(long schoollistId);
    public Task<bool> Validation(PostClassDepartment classdep);
    public Task<bool> Validation(long Id, PatchClassDepartment classdep);
    public Task<bool> Validation(long ClassDepartmentId, long userId);
}
