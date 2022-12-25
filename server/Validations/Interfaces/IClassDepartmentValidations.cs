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
    public Task<bool> Validation(Models.DTOs.ClassDepartment.PostClassDepartment classdep);
    public Task<bool> Validation(Models.DTOs.ClassDepartment.PatchClassDepartment classdep);
    public Task<bool> Validation(long ClassDepartmentId, long userId);
}
