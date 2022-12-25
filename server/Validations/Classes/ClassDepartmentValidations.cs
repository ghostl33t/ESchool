using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using server.Database;
using server.Validations.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace server.Validations.Classes
{
    public class ClassDepartmentValidations : IClassDepartmentValidations
    {
        private readonly DBMain _dbMain;
        private readonly DBRegistries _dbRegistries;
        public string validationMessage { get; set; }
        public int code { get; set; }
        public ClassDepartmentValidations(DBMain DbMain, DBRegistries dBRegistries)
        {
            this._dbMain = DbMain;
            this._dbRegistries = dBRegistries;
        }

        public async Task<bool> ValidateCreator(long CreatedById)
        {
            try
            {
                var creator =  await _dbMain.Users.FirstOrDefaultAsync(s => s.Id == CreatedById && s.Deleted == 0);
                if (creator == null || creator.UserType != 0)
                {
                    return await Task.FromResult(false);
                }
                return await Task.FromResult(true);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<bool> ValidateSerialUnique(string classDepartmentserialNumber)
        {
            try
            {
                var schoolExists = await _dbMain.ClassDepartments.FirstOrDefaultAsync(s => s.SerialNumber == classDepartmentserialNumber && s.Deleted == 0 && s.SerialNumber != classDepartmentserialNumber);
                if (schoolExists != null)
                {
                    return await Task.FromResult(false);
                }
                return await Task.FromResult(true);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<bool> ValidateClassSerialNumber(string classDepartmentserialNumber)
        {
            if (classDepartmentserialNumber.Length < 3 || classDepartmentserialNumber.Length > 5)
            {
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }
        public async Task<bool> ValidateClassName(string classDepartmentname)
        {
            if (classDepartmentname.Length < 5 || classDepartmentname.Length > 15)
            {
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }
        public async Task<bool> ValidateSchoolListId(long schoollistId) //TODO ovdje kontrolisati da li tip skole postoji u registrima
        {
            var schoollist = await _dbRegistries.SchoolList.FirstOrDefaultAsync(s => s.Id == schoollistId);
            if (schoollist == null)
            {
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }
        public async Task<bool> Validation(Models.DTOs.ClassDepartment.PostClassDepartment classDepartment)
        {
            validationMessage = "";
            code = 0;
            if (await ValidateCreator(classDepartment.CreatorId) == false)
            {
                code = 401;
                validationMessage = await Task.FromResult("You don't have permision to create class!");
            }
            if (await ValidateSerialUnique(classDepartment.SerialNumber) == false)
            {
                code = 400;
                validationMessage = await Task.FromResult("Class with this serial number already exists in database!");
            }
            if (await ValidateClassSerialNumber(classDepartment.SerialNumber) == false)
            {
                code = 400;
                validationMessage = await Task.FromResult("Class serial number is invalid!");
            }

            if (await ValidateClassName(classDepartment.Name) == false)
            {
                code = 400;
                validationMessage = await Task.FromResult("Class name is invalid");
            }
            if (await ValidateSchoolListId(classDepartment.SchoolListId) == false)
            {
                code = 400;
                validationMessage = await Task.FromResult("School List ID is invalid!");
            }
            if (code != 0) return false;
            code = 201;
            validationMessage = "OK";
            return true;
        }
        public async Task<bool> Validation(Models.DTOs.ClassDepartment.PatchClassDepartment classDepartment)
        {
            validationMessage = "";
            code = 0;
            if (await ValidateCreator(classDepartment.UpdatedById) == false)
            {
                code = 401; 
                validationMessage = await Task.FromResult("You don't have permision to modify class!");
            }
            if (await ValidateSerialUnique(classDepartment.SerialNumber) == false)
            {
                code = 400;
                validationMessage = await Task.FromResult("Class with this serial number already exists in database!");
            }
            if (await ValidateClassSerialNumber(classDepartment.SerialNumber) == false)
            {
                code = 400;
                validationMessage = await Task.FromResult("Class serial number is invalid!");
            }

            if (await ValidateClassName(classDepartment.Name) == false)
            {
                code = 400;
                validationMessage = await Task.FromResult("Class name is invalid");
            }
            if (await ValidateSchoolListId(classDepartment.SchoolListId) == false)
            {
                code = 400;
                validationMessage = await Task.FromResult("School List ID is invalid!");
            }
            if (code != 0) return false;
            code = 200;
            validationMessage = "OK";
            return true;
        }
        public async Task<bool> Validation(long ClassDepartmentId, long UserId)
        {
            var school = await _dbMain.ClassDepartments.FirstOrDefaultAsync(s => s.ID == ClassDepartmentId);
            var Administrator = await _dbMain.Users.FirstOrDefaultAsync(s => s.Id == UserId);
            code = 0;
            if (Administrator != null)
            {
                if (Administrator.UserType != 0)
                {
                    code = 401;
                    validationMessage =  await Task.FromResult("Unauthorized!");
                }
                if (school == null)
                {
                    code = 400;
                    validationMessage = await Task.FromResult("ClassDepartment not found!");
                }
            }
            if (code != 0) return false;
            validationMessage = "DELETED";
            code = 204;
            return true;
        }
    }
}
