using server.Database;
using server.Models.DTOs.StudentGrades;
using server.Validations.Interfaces;

namespace server.Validations.Classes;

public class StudentGradesValidations : IStudentGradesValidations
{
    private readonly DBMain _dbMain;
    public int code { get; set; }
    public string validationMessage { get; set; }

    public StudentGradesValidations(DBMain dbmain)
    {
        _dbMain = dbmain;
    }

    public async Task<bool> ValidateProfessor(long classDepId, long professorId);
    public async Task<bool> ValidateSubject(long classDepId, long subjectId);
    public async Task<bool> ValidateGrade(int grade);
    public async Task<bool> ValidateStudent(long studentId);
    public async Task<bool> ValidateDescription(string description);

    public async Task<bool> Validate(PostStudentGrades studentGrade);
    public async Task<bool> Validate(long Id, PatchStudentGrades studentGrade);
    public async Task<bool> Validate(long Id, long professorId);
}
