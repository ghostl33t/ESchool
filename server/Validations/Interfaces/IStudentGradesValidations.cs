namespace server.Validations.Interfaces;
public interface IStudentGradesValidations
{
    public string validationMessage { get; set; }
    public int code { get; set; }
    public Task<bool> StudentExist(long studentId);
    public Task<bool> StudentSubject(long studentid, long subjectId);
    public Task<bool> ValidateProfessor(long studentid, long professorId);
    public bool CheckIfGradeIsValidated(int validatedGrade);
    public Task<bool> Validations(Models.DTOs.StudentGrades.PostStudentGrades create);
    public Task<bool> Validations(Models.DTOs.StudentGrades.PatchStudentGrades create);
    public Task<bool> Validations(long gradeId, long professorId);

}
