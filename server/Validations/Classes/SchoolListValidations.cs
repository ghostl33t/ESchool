using Microsoft.EntityFrameworkCore;
using server.Database;
using server.Validations.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace server.Validations.Classes
{
    public class SchoolListValidations : ISchoolListValidations
    {
        private readonly DBMain _dbMain;
        private readonly DBRegistries _dbRegistries;
        public string validationMessage { get; set; } = String.Empty;
        public int code { get; set; }
        public SchoolListValidations(DBMain dbMain, DBRegistries dbRegistries)
        {
            this._dbMain = dbMain;
            this._dbRegistries = dbRegistries;
        }

        public async Task<bool> ValidateSchoolCreator(long CreatedById)
        {
            var creator = await _dbMain.Users.FirstOrDefaultAsync(s => s.Id == CreatedById && s.Deleted == 0);
            if (creator == null || creator.UserType != 0)
            {
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }
        public async Task<bool> ValidateSerialUnique(string serialNumber)
        {
            var schoolExists = _dbRegistries.SchoolList.FirstOrDefault(s => s.SerialNumber == serialNumber && s.Deleted == 0);
            if (schoolExists != null)
            {
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }
        public async Task<bool> ValidateSchoolSerialNumber(string serialNumber)
        {
            if (serialNumber.Length < 3 || serialNumber.Length > 5)
            {
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }
        public async Task<bool> ValidateSchoolName(string schoolName)
        {
            if (schoolName.Length < 5 || schoolName.Length > 15)
            {
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }
        public async Task<bool> ValidateSchoolType(int schoolType)
        {
            if (schoolType < 0 || schoolType > 1)
            {
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }
        public async Task<bool> Validation(Models.DTOs.SchoolList.PostSchoolList school)
        {
            code = 0;
            if (await ValidateSchoolCreator(school.CreatedById) == false)
            {
                code = 401;
                validationMessage =  await Task.FromResult("You don't have permision to create school!");
            }
            if (await ValidateSerialUnique(school.SerialNumber) == false)
            {
                code = 400;
                validationMessage = await Task.FromResult("School with this serial number already exists in database!");
            }
            if (await ValidateSchoolSerialNumber(school.SerialNumber) == false)
            {
                code = 400;
                validationMessage = await Task.FromResult("Schools serial number is invalid!");
            }
            if (await ValidateSchoolName(school.Name) == false)
            {
                code = 400;
                validationMessage = await Task.FromResult("School name is invalid");
            }
            if (await ValidateSchoolType(school.SchoolType) == false)
            {
                validationMessage = await Task.FromResult("School type is invalid!");
            }
            if (code != 0) { return false; }

            code = 201;
            validationMessage = "School added succesfuly!";
            return true;
        }
        public async Task<bool> Validation(Models.DTOs.SchoolList.PatchUpdate school)
        {
            var schoolexist = await _dbRegistries.SchoolList.FirstOrDefaultAsync(s => s.Id == school.Id);
            code = 0;
            if (await ValidateSchoolCreator(school.UpdatedById) == false)
            {
                code = 401;
                validationMessage = await Task.FromResult("You don't have permision to create school!");
            }
            if(schoolexist == null)
            {
                code = 400;
                validationMessage = await Task.FromResult("School doesn't exist!");
            }
            else
            {
                if (schoolexist.SerialNumber != school.SerialNumber)
                {
                    if (await ValidateSerialUnique(school.SerialNumber) == false)
                    {
                        code = 400;
                        validationMessage = await Task.FromResult("School with this serial number already exists in database!");
                    }
                    if (await ValidateSchoolSerialNumber(school.SerialNumber) == false)
                    {
                        code = 400;
                        validationMessage = await Task.FromResult("Schools serial number is invalid!");
                    }
                    if (schoolexist.Name != school.Name)
                    {
                        if (await ValidateSchoolName(school.Name) == false)
                        {
                            code = 400;
                            validationMessage = await Task.FromResult("School name is invalid");
                        }
                    }
                    if (await ValidateSchoolType(school.SchoolType) == false)
                    {
                        validationMessage = await Task.FromResult("School type is invalid!");
                    }
                }
            }
            if (code != 0) { return false; }
            code = 201;
            
            validationMessage ="School updated succesfuly!";
            return true;
        }
        public async Task<bool> Validation(long schoolId, long AdministratorId)
        {
            var school = await this._dbRegistries.SchoolList.FirstOrDefaultAsync(s => s.Id == schoolId);
            var Administrator = await this._dbMain.Users.FirstOrDefaultAsync(s => s.Id == AdministratorId);
            code = 0;
            if (Administrator != null)
            {
                if (Administrator.UserType != 0)
                {
                    code = 401;
                    validationMessage = await Task.FromResult("Unauthorized!");
                }
                if (school == null)
                {
                    code = 400;
                    validationMessage = await Task.FromResult("School not found!");
                }
            }
            if(code != 0) { return false; }
            validationMessage = "School deleted succesfuly!";
            code = 204;
            return true;

        }
    }
}
