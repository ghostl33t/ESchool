using Microsoft.EntityFrameworkCore;
using server.Database;
using server.Models.DTOs.ClassDepartment;
using server.Models.DTOs.Subject;
using server.Models.DTOs.UsersDTO;
using server.Validations.Interfaces;
using System.Security.Claims;

namespace server.Validations.Classes
{
    public class CDSPValidations : ICDSPValidations
    {
        private readonly DBMain _dbMain;
        private readonly DBRegistries _dbRegistries;
        public string validationMessage { get; set; } = String.Empty;
        public int code { get; set; }
        public CDSPValidations(DBMain dbMain, DBRegistries dbRegistries)
        {
            _dbMain = dbMain;
            _dbRegistries = dbRegistries;
        }
        public async Task<bool> ValidateCreator(long createdById)
        {
            var user = await _dbMain.Users.FirstOrDefaultAsync(s => s.Id == createdById);
            if (user == null || user.UserType != 0 || user.UserType != 1)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> ValidateSubject(long subjectId)
        {
            var subject = await _dbRegistries.Subjects.FirstOrDefaultAsync(s => s.Id == subjectId);
            if (subject == null)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> ValidateProfessor(long professorId)
        {
            var user = await _dbMain.Users.FirstOrDefaultAsync(s => s.Id == professorId);
            if (user == null || user.UserType != 1)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> ValidateClassDepartment(long classDepId)
        {
            var classDep = await _dbMain.ClassDepartments.FirstOrDefaultAsync(s => s.ID == classDepId);
            if (classDep == null)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> Validate(long creatorId, long subjectId , long professorId, long classDepId)
        {
            code = 0;
            if (await ValidateCreator(creatorId) == false)
            {
                validationMessage = "Creator is not valid";
                code = 401;
            }
            if (await ValidateSubject(subjectId) == false)
            {
                validationMessage = "Subject is not Valid";
                code = 400;
            }
            if (await ValidateProfessor(professorId) == false)
            {
                validationMessage = "Professor is not Valid";
                code = 400;
            }
            if (await ValidateClassDepartment(classDepId) == false)
            {
                validationMessage = "Class department is not valid";
                code = 400;
            }
            if(code != 0)
            {
                return false;
            }
            code = 201;
            validationMessage = "Relation between ClassDep-Prof-Subject created succesfully!";
            return true;
        }
        public async Task<bool> Validate(long Id, long administratorId)
        {
            code = 0;
            var exist = await _dbMain.ClassDepartmentSubjectProfessors.FirstOrDefaultAsync(s => s.Id == Id && s.Deleted == 0);
            if (exist == null)
            {
                code = 400;
                validationMessage = "Unable to find CDSP";
            }
            var admin = await _dbMain.Users.FirstOrDefaultAsync(s => s.Id == administratorId);
            if(admin == null || admin.UserType != 0){
                code = 401;
                validationMessage = "Unauthorized";
            }
            if(code != 0)
            {
                return false;
            }
            code = 201;
            validationMessage = "Relation between ClassDep-Prof-Subject created succesfully!";
            return true;
        }
    }
}
