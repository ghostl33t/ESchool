using server.Models.DTOs.StudentDetails;

namespace server.Validations.Interfaces;
public interface IStudentDetailsValidations
{
    int code { get; set; }
    string validationMessage { get; set; }
    //Validiranje da li je kreator administrator
    public Task<bool> ValidateCreator(long creatorId);
    //Validiranje da li postoji student
    public Task<bool> ValidateStudent(long studId);
    //Validiranje da li postoji ClassDepartment
    public Task<bool> ValidateClassDepartment(long cdId);
    //Validiranje roditelja 1 i 2 samo pozivat 2 put metode 
    public Task<bool> ValidateParent(long parentId);
    //Validiranje discipline od 1 do 5 
    public Task<bool> ValidateDiscipline(int? discipline);

    public Task<bool> Validate(PostStudentDetails studentDetails);
    public Task<bool> Validate(long studentDetailsId, PatchStudentDetails studentDetails);
    public Task<bool> Validate(long studentDetailsId, long administratorId);
}
