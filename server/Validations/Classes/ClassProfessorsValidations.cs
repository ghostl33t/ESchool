using Microsoft.EntityFrameworkCore;
using server.Database;
using server.Models.DTOs.ClassProfessors;
using server.Validations.Interfaces;

namespace server.Validations.Classes;

public class ClassProfessorsValidations : IClassProfessorsValidations
{
    protected readonly DBMain _dbMain;
    public string validationMessage { get; set; } = string.Empty;
    public int code { get; set; }

    public ClassProfessorsValidations(DBMain dbMain)
    {
        _dbMain = dbMain;
    }

    public async Task<bool> ValidateCreator(long leaderId, long classDepId)
    {
        var classDep = await _dbMain.ClassDepartments.AsNoTracking().Include(s=> s.LeaderProfessor).FirstOrDefaultAsync(s => s.ID == classDepId && s.Deleted == 0);
        if (classDep != null)
        {
            if (classDep.LeaderProfessor.Id != leaderId) { return false; }
        }
        else
        {
            return false;
        }
        return true;
    }
    public async Task<bool> ValidateProfessor(long Id)
    {
        var subject = await _dbMain.Users.AsNoTracking().FirstOrDefaultAsync(s => s.Id == Id && s.Deleted == 0);
        if (subject == null)
        {
            return false;
        }
        return true;
    }
    public async Task<bool> ValidateClassDepartment(long Id)
    {
        var classdep = await _dbMain.ClassDepartments.AsNoTracking().FirstOrDefaultAsync(s => s.ID == Id && s.Deleted == 0);
        if (classdep == null)
        {
            return false;
        }
        return true;
    }
    public async Task<bool> Validate(PostClassProfessors classProfessors)
    {
        code = 0;
        if (await ValidateCreator(classProfessors.CreatedById_, classProfessors.ClassDepartmentId_) == false)
        {
            code = 401;
            validationMessage = "Unauthorized!";
        }
        if (await ValidateClassDepartment(classProfessors.ClassDepartmentId_) == false)
        {
            code = 400;
            validationMessage = "Unable to find Class department!";
        }
        if (await ValidateProfessor(classProfessors.ProfessorId_) == false)
        {
            code = 400;
            validationMessage = "Unable to find professor!";
        }
        if (code != 0) { return false; }
        code = 201;
        validationMessage = String.Format("Relation between Class department '{0}' & professor '{1}' created successfuly!", classProfessors.ClassDepartmentId_, classProfessors.ProfessorId_);
        return true;
    }
    public async Task<bool> Validate(long Id, PatchClassProfessors classProfessors)
    {
        code = 0;
        if (await ValidateCreator(classProfessors.UpdatedById_, classProfessors.ClassDepartmentId_) == false)
        {
            code = 401;
            validationMessage = "Unauthorized!";
        }
        if (await ValidateClassDepartment(classProfessors.ClassDepartmentId_) == false)
        {
            code = 400;
            validationMessage = "Unable to find Class department!";
        }
        if (await ValidateProfessor(classProfessors.ProfessorId_) == false)
        {
            code = 400;
            validationMessage = "Unable to find professor!";
        }
        if (code != 0) { return false; }
        code = 200;
        validationMessage = String.Format("Relation between Class department '{0}' & professor '{1}' updated successfuly!", classProfessors.ClassDepartmentId_, classProfessors.ProfessorId_);
        return true;
    }
    public async Task<bool> Validate(long Id, long leaderId)
    {
        var classProfessor = await _dbMain.ClassProfessors.AsNoTracking().FirstOrDefaultAsync(s => s.ID == Id);
        var classDep = await _dbMain.ClassDepartments.AsNoTracking().FirstOrDefaultAsync(s => s.LeaderProfessor.Id == leaderId && Id == classProfessor.ClassDepartment.ID);
        if (classProfessor == null || classDep == null)
        {
            code = 401;
            validationMessage = "Unauthorized";
        }
        if (code != 0) { return false; }
        code = 200;
        validationMessage = String.Format("Relation between Class department '{0}' & classProfessors '{1}' deleted successfuly!", classProfessor.ClassDepartment.ID, classProfessor.Professor.Id);
        return true;
    }
}
