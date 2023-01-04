using Microsoft.EntityFrameworkCore;
using server.Database;
using server.Models.DTOs.StudentGrades;
using server.Validations.Interfaces;

namespace server.Validations.Classes;

public class StudentGradesValidations : IStudentGradesValidations
{
    private readonly DBMain _dbMain;
    public int code { get; set; }
    public string validationMessage { get; set; } = String.Empty;

    public StudentGradesValidations(DBMain dbmain)
    {
        _dbMain = dbmain;
    }
    public async Task<bool> ValidateGrade(int grade)
    {
        if(grade > 5 || grade < 1) { return await Task.FromResult(false); ; }
        return await Task.FromResult(true);
    }
    public async Task<bool> ValidateDescription(string description)
    {
        if(description.Trim() == "") { return await Task.FromResult(false); ; }
        return await Task.FromResult(true);
    }

    public async Task<bool> Validate(PostStudentGrades studentGrade)
    {
        code = 0;
        var student = await _dbMain.Users.AsNoTracking().FirstOrDefaultAsync(s => s.Id == studentGrade.StudentId);
        if(student != null){

        
        var studentDetails = await _dbMain.StudentsDetails.AsNoTracking().Include(s=>s.ClassDepartment).FirstOrDefaultAsync(s => s.StudentId_ == student.Id);
        if(studentDetails != null)
        {
            var classProfessor = await _dbMain.ClassProfessors.AsNoTracking().FirstOrDefaultAsync(s => s.Professor.Id == studentGrade.ProfessorId && s.ClassDepartment.ID == studentDetails.ClassDepartment.ID);
            if(classProfessor != null)
            {
                var classSubjects = await _dbMain.ClassSubjects.AsNoTracking().FirstOrDefaultAsync(s => s.Subject.Id == studentGrade.SubjectId && s.ClassDepartment.ID == studentDetails.ClassDepartment.ID);
                if (classSubjects != null)
                {
                    var professorSubject = await _dbMain.ProfessorSubjects.AsNoTracking().FirstOrDefaultAsync(s => s.Professor.Id == studentGrade.ProfessorId && s.SubjectId == studentGrade.SubjectId);
                    if (professorSubject == null)
                    {
                        code = 400;
                        validationMessage = "Invalid data";
                    }
                }
                else
                {
                    code = 400;
                    validationMessage = "Invalid data";
                }
            }
            else {
                code = 400;
                validationMessage = "Invalid data";
            }
        }
        else
        {
            code = 400;
            validationMessage = "Invalid data";
        }
        }
        else{
                code = 400;
                validationMessage = "Invalid data";
            
        }
        if(await ValidateGrade(studentGrade.Grade) == false)
        {
            code = 400;
            validationMessage = "Invalid grade!";
        }
        if(await ValidateDescription(studentGrade.Description) == false)
        {
            code = 400;
            validationMessage = "Invalid description!";
        }
        if(code != 0) { return false; }
        code = 201;
        validationMessage = String.Format("Professor '{0}' has added grade for student '{1}' for subject '{2}' ",studentGrade.ProfessorId,studentGrade.StudentId,studentGrade.SubjectId);
        return true;
    }
    public async Task<bool> Validate(long Id, PatchStudentGrades studentGrade)
    {
        code = 0;
        if(await _dbMain.StudentGrades.AsNoTracking().FirstOrDefaultAsync(s=>s.Id == Id) == null)
        {
            code = 400;
            validationMessage = "Grade doesn't exists in database !";
        }
        var student = await _dbMain.Users.AsNoTracking().FirstOrDefaultAsync(s => s.Id == studentGrade.StudentId);
        var studentDetails = await _dbMain.StudentsDetails.AsNoTracking().Include(s => s.ClassDepartment).FirstOrDefaultAsync(s => s.StudentId_ == student.Id);
        if (studentDetails != null)
        {
            var classProfessor = await _dbMain.ClassProfessors.AsNoTracking().FirstOrDefaultAsync(s => s.Professor.Id == studentGrade.ProfessorId && s.ClassDepartment.ID == studentDetails.ClassDepartment.ID);
            if (classProfessor != null)
            {
                var classSubjects = await _dbMain.ClassSubjects.AsNoTracking().FirstOrDefaultAsync(s => s.Subject.Id == studentGrade.SubjectId && s.ClassDepartment.ID == studentDetails.ClassDepartment.ID);
                if (classSubjects != null)
                {
                    var professorSubject = await _dbMain.ProfessorSubjects.AsNoTracking().FirstOrDefaultAsync(s => s.Professor.Id == studentGrade.ProfessorId && s.SubjectId == studentGrade.SubjectId);
                    if (professorSubject == null)
                    {
                        code = 400;
                        validationMessage = "Invalid data";
                    }
                }
                else
                {
                    code = 400;
                    validationMessage = "Invalid data";
                }
            }
            else
            {
                code = 400;
                validationMessage = "Invalid data";
            }
        }
        else
        {
            code = 400;
            validationMessage = "Invalid data";
        }
        if (await ValidateGrade(studentGrade.Grade) == false)
        {
            code = 400;
            validationMessage = "Invalid grade!";
        }
        if (await ValidateDescription(studentGrade.Description) == false)
        {
            code = 400;
            validationMessage = "Invalid description!";
        }
        if (code != 0) { return false; }
        code = 200;
        validationMessage = String.Format("Professor '{0}' has updated grade for student '{1}' for subject '{2}' ", studentGrade.ProfessorId, studentGrade.StudentId, studentGrade.SubjectId);
        return true;
    }
    public async Task<bool> Validate(long Id, long professorId)
    {
        code = 400;
        var grade = await _dbMain.StudentGrades.AsNoTracking().FirstOrDefaultAsync(s => s.Id == Id && s.ProfessorId == professorId);
        if(grade == null)
        {
            code = 401;
            validationMessage = "Unauthorized!";
        }
        if (code != 0) { return false; }
        code = 200;
        validationMessage = String.Format("Professor '{0}' has deleted grade for student '{1}' for subject '{2}' ", grade.ProfessorId, grade.StudentId, grade.SubjectId);
        return true;
    }
}
