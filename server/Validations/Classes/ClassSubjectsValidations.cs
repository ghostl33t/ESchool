using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using server.Database;
using server.Models.DTOs.ClassSubjects;
using server.Validations.Interfaces;

namespace server.Validations.Classes
{
    public class ClassSubjectsValidations : IClassSubjectsValidations
    {
        protected readonly DBMain _dbMain;
        public string validationMessage { get; set; } = string.Empty;
        public int code { get; set; }

        public ClassSubjectsValidations(DBMain dbMain)
        {
            _dbMain = dbMain;
        }

        public async Task<bool> ValidateCreator(long leaderId, long classDepId)
        {
            var classDep = await _dbMain.ClassDepartments.AsNoTracking().Include(s=>s.LeaderProfessor).FirstOrDefaultAsync(s => s.ID == classDepId && s.Deleted == 0);
            if(classDep != null)
            {
                if(classDep.LeaderProfessor.Id != leaderId) { return false; }
            }
            else
            {
                return false;
            }
            return true;
        }      
        public async Task<bool> ValidateSubject(long Id)
        {
            var subject = await _dbMain.Subjects.AsNoTracking().FirstOrDefaultAsync(s => s.Id == Id && s.Deleted == 0);
            if(subject == null)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> ValidateClassDepartment(long Id)
        {
            var classdep = await _dbMain.ClassDepartments.AsNoTracking().FirstOrDefaultAsync(s => s.ID == Id && s.Deleted == 0);
            if(classdep == null)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> Validate(PostClassSubjects classSubject)
        {
            code = 0;
            if(await ValidateCreator(classSubject.CreatedById_,classSubject.ClassDepartmentId_) == false)
            {
                code = 401;
                validationMessage = "Unauthorized!";
            }
            if(await ValidateClassDepartment(classSubject.ClassDepartmentId_) == false)
            {
                code = 400;
                validationMessage = "Unable to find Class department!";
            }
            if(await ValidateSubject(classSubject.SubjectId_) == false)
            {
                code = 400;
                validationMessage = "Unable to find subject!";
            }
            if(code != 0) { return false; }
            code = 201;
            validationMessage = String.Format("Relation between Class department '{0}' & subject '{1}' created successfuly!", classSubject.ClassDepartmentId_, classSubject.ClassDepartmentId_);
            return true;
        }
        public async Task<bool> Validate(long Id, PatchClassSubjects classSubject)
        {
            code = 0;
            if (await ValidateCreator(classSubject.UpdatedById_, classSubject.ClassDepartmentId_) == false)
            {
                code = 401;
                validationMessage = "Unauthorized!";
            }
            if (await ValidateClassDepartment(classSubject.ClassDepartmentId_) == false)
            {
                code = 400;
                validationMessage = "Unable to find Class department!";
            }
            if (await ValidateSubject(classSubject.SubjectId_) == false)
            {
                code = 400;
                validationMessage = "Unable to find subject!";
            }
            if (code != 0) { return false; }
            code = 200;
            validationMessage = String.Format("Relation between Class department '{0}' & subject '{1}' updated successfuly!", classSubject.ClassDepartmentId_, classSubject.ClassDepartmentId_);
            return true;
        }
        public async Task<bool> Validate(long Id, long leaderId)
        {
            var classSubject = await _dbMain.ClassSubjects.AsNoTracking().FirstOrDefaultAsync(s=>s.ID == Id);
            var classDep = await _dbMain.ClassDepartments.AsNoTracking().FirstOrDefaultAsync(s => s.LeaderProfessor.Id == leaderId && Id == classSubject.ClassDepartmentId_);
            if(classSubject == null || classDep == null)
            {
                code = 401;
                validationMessage = "Unauthorized";
            }
            if(code != 0) { return false; }
            code = 200;
            validationMessage = String.Format("Relation between Class department '{0}' & subject '{1}' deleted successfuly!", classSubject.ClassDepartmentId_, classSubject.ClassDepartmentId_);
            return true;
        }

    }
}
