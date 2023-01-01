using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using server.Database;
using server.Models.Domain;
using server.Models.DTOs.ProfessorSubjects;
using server.Validations.Interfaces;

namespace server.Validations.Classes;

public class ProfessorSubjectsValidations : IProfessorSubjectsValidation
{
    private readonly DBMain _dbMain;
    private readonly DBRegistries _dbRegistries;
    public ProfessorSubjectsValidations(DBMain dbMain,DBRegistries dbRegistries)
    {
        _dbMain = dbMain;
        _dbRegistries = dbRegistries;
    }
    public string validationMessage { get; set; }
    public int code { get; set; }

    //Onaj koji dodaje mora biti administrator
    public async Task<bool> ValidateCreator(long creatorId)
    {
        var creator = await _dbMain.Users.AsNoTracking().FirstOrDefaultAsync(s => s.Id == creatorId && s.Deleted == 0);
        if(creator != null)
        {
            if (creator.UserType != 0)
            {
                return false;
            }
        }
        else { return false; }
        return true;
    }
    //Moguce dodati samo korisnike koji su tipa 1					
    public async Task<bool> ValidateProfessorType(long profID)
    {
        var prof = await _dbMain.Users.AsNoTracking().FirstOrDefaultAsync(s => s.Id == profID && s.Deleted == 0);
        if(prof != null)
        {
            if(prof.UserType != 1)
            {
                return false;
            }
        }
        else { return false; }
        return true;
    }
    //Nije moguce dodati 2 puta istog profesora za isti predmet
    public async Task<bool> ValidateProfessorRepeating(long professorSubjectId, long professorId, long subjectId)
    {
        if(professorSubjectId != 0)
        {
            var exist = await _dbMain.ProfessorSubjects.FirstOrDefaultAsync(s => s.Professor.Id == professorId && s.SubjectId == subjectId && s.ID != professorSubjectId);
            if(exist != null)
            {
                return false;
            }
        }
        else if (professorSubjectId == 0)
        {
            var exist = await _dbMain.ProfessorSubjects.FirstOrDefaultAsync(s => s.Professor.Id == professorId && s.SubjectId == subjectId);
            if (exist != null)
            {
                return false;
            }
        }
        return true;
    }
    //Validacija predmeta
    public async Task<bool> ValidateSubject(long subjectId)
    {
        if( await _dbRegistries.Subjects.AsNoTracking().FirstOrDefaultAsync(s => s.Id == subjectId && s.Deleted == 0) == null)
        {
            return false;
        }
        return true;
    }
    public async Task<bool> Validate(PostProfessorSubjects professorSubj)
    {
        code = 0;
        if( await ValidateCreator(professorSubj.CreatedById_) == false)
        {
            code = 401;
            validationMessage = "Unauthorized";
        }
        else if(await ValidateProfessorType(professorSubj.ProfessorId_) == false)
        {
            code = 400;
            validationMessage = "Invalid professor id ";
        }
        else if (await ValidateProfessorRepeating(0,professorSubj.ProfessorId_, professorSubj.SubjectId) == false)
        {
            code = 400;
            validationMessage = "Professor for this subject is already defined";
        }
        else if (await ValidateSubject(professorSubj.SubjectId) == false)
        {
            code = 400;
            validationMessage = "Invalid subject!";
        }
        if(code != 0) { return false;}
        code = 201;
        validationMessage = String.Format("Relation between professor '{0}' and subject '{1}' created!",professorSubj.ProfessorId_, professorSubj.SubjectId);
        return true;
    }
    public async Task<bool> Validate(long Id, PatchProfessorSubjects professorSubj)
    {
        code = 0;
        if(await _dbMain.ProfessorSubjects.AsNoTracking().FirstOrDefaultAsync(s=>s.ID == Id) == null)
        {
            code = 400;
            validationMessage = "No relation!";
        }
        if (await ValidateCreator(professorSubj.UpdatedById) == false)
        {
            code = 401;
            validationMessage = "Unauthorized";
        }
        else if (await ValidateProfessorType(professorSubj.ProfessorId_) == false)
        {
            code = 400;
            validationMessage = "Invalid professor id ";
        }
        else if (await ValidateProfessorRepeating(1, professorSubj.ProfessorId_, professorSubj.SubjectId) == false)
        {
            code = 400;
            validationMessage = "Professor for this subject is already defined";
        }
        else if (await ValidateSubject(professorSubj.SubjectId) == false)
        {
            code = 400;
            validationMessage = "Invalid subject!";
        }
        if (code != 0) { return false; }
        code = 201;
        validationMessage = String.Format("Relation between professor '{0}' and subject '{1}' updated!", professorSubj.ProfessorId_, professorSubj.SubjectId);
        return true;
    }
    public async Task<bool> Validate(long Id, long AdministratorId)
    {
        code = 0;
        var profsubj = await _dbMain.ProfessorSubjects.AsNoTracking().FirstOrDefaultAsync(s => s.ID == Id);
        if(profsubj == null)
        {
            code = 400;
            validationMessage = "Relation doesn't exists in database";
        }
        if(await ValidateCreator(AdministratorId) == false)
        {
            code = 401;
            validationMessage = "Unauthorized";
        }
        if (code != 0) { return false; }
        code = 201;
        validationMessage = String.Format("Relation between professor '{0}' and subject '{1}' deleted!", profsubj.Professor.Id, profsubj.SubjectId);
        return true;
    }
}
