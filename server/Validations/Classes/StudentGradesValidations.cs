using Microsoft.EntityFrameworkCore;
using server.Database;
using server.Models.Domain;
using server.Profiles;
using server.Validations.Interfaces;

namespace server.Validations.Classes;

public class StudentGradesValidations : IStudentGradesValidations
{
    public string validationMessage { get; set; } = String.Empty;
    public int code { get; set; }
    private readonly DBMain _dbMain;
    private readonly DBRegistries _dbRegistries;
    public StudentGradesValidations(DBMain dbMain, DBRegistries dbRegistries)
    {
        this._dbMain = dbMain;
        this._dbRegistries = dbRegistries;
    }
    public async Task<bool> StudentExist(long studentId)
    {
        var studentExist = await _dbMain.Users.FirstOrDefaultAsync(s => s.Id == studentId && s.UserType == 2);
        if (studentExist != null)
            return true;
        return false;
    }
    public async Task<bool> StudentSubject(long studentid, long subjectId)
    {
        var studentDetails = await _dbMain.StudentsDetails.FirstOrDefaultAsync(s => s.Student.Id == studentid);
        if (studentDetails == null)
        {
            return false;
        }
        var studentHaveSubject = await _dbMain.ClassDepartmentSubjectProfessors.FirstOrDefaultAsync(s => s.SubjectID == subjectId && s.ClassDepartment.ID == studentDetails.ClassDepartment.ID);
        if (studentHaveSubject != null)
        {
            return true;
        }
        return false;
    }
    public async Task<bool> ValidateProfessor(long studentid, long professorId)
    {
        var studentDetails = await _dbMain.StudentsDetails.FirstOrDefaultAsync(s => s.Student.Id == studentid);
        var validProfessor = await _dbMain.ClassDepartmentSubjectProfessors.FirstOrDefaultAsync(s => s.UserProfessor.Id == professorId && s.ClassDepartment.ID == studentDetails.ClassDepartment.ID);
        if (validProfessor != null)
        {
            return true;
        }
        return false;
    }
    public bool CheckIfGradeIsValidated(int validatedGrade)
    {
        if (validatedGrade == 0)
        {
            return true;
        }
        return false;
    }
    public async Task<bool> Validations(Models.DTOs.StudentGrades.PostStudentGrades create) 
    {
        code = 0;
        if (await StudentExist(create.StudentId) == false)
        {
            validationMessage = "Student is not valid!";
            code = 400;
        }
        else if (await StudentSubject(create.StudentId, create.CDSPID) == false)
        {
            validationMessage = "Student doesn't have this subject!";
            code = 400;
        }
        else if (await ValidateProfessor(create.StudentId, create.CDSPID) == false)
        {
            validationMessage = "Professor is invalid!";
            code = 400;
        }
        if(code != 0) { return false; }
        code = 201;
        validationMessage = "Grade added succesfuly";
        return true;
    }
    public async Task<bool> Validations(long Id, Models.DTOs.StudentGrades.PatchStudentGrades grade)
    {
        code = 0;
        if (await StudentExist(grade.StudentId) == false)
        {
            validationMessage = "Student is not valid!";
            code = 400;
        }
        else if (await StudentSubject(grade.StudentId, grade.CDSPID) == false)
        {
            validationMessage = "Student doesn't have this subject!";
            code = 400;
        }
        else if (await ValidateProfessor(grade.StudentId, grade.CDSPID) == false)
        {
            validationMessage = "Professor is invalid!";
            code = 400;
        }
        if (code != 0) { return false; }
        code = 200;
        validationMessage = "Grade updated successfuly";
        return true;
    }
    public async Task<bool> Validations(long gradeId, long professorId)
    {
        code = 0;
        var validGrade = await _dbMain.StudentsGrades.FirstOrDefaultAsync(s => s.Id == gradeId);
        if(validGrade == null)
        {
            code = 400;
            validationMessage = "Grade not found";
        }
        else
        {
            if (CheckIfGradeIsValidated(validGrade.Validated) == false)
            {
                code = 400;
                validationMessage = "Grade is validated.";
            }
            //TODO Validirati da li professorId zaista upisao ovu ocjenu
        }
        if (code != 0) { return false; }
        code = 201;
        validationMessage = "Grade deleted successfuly";
        return true;
    }
}
