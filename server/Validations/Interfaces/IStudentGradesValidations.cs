using server.Models.DTOs.StudentGrades;

namespace server.Validations.Interfaces;

public interface IStudentGradesValidations
{
    public int code { get; set; }
    public string validationMessage { get; set; }
    public Task<bool> ValidateGrade(int grade);
    public Task<bool> ValidateDescription(string description);
    public Task<bool> Validate(PostStudentGrades studentGrade);
    public Task<bool> Validate(long Id, PatchStudentGrades studentGrade);
    public Task<bool> Validate(long Id, long professorId);
}