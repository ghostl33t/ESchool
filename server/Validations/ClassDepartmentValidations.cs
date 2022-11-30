using Microsoft.EntityFrameworkCore;
using server.Database;

namespace server.Validations
{
    public class ClassDepartmentValidations : IClassDepartmentValidations
    {
        private readonly DBMain DbMain;
        private readonly DBRegistries DBRegistries;
        public bool validationResult { get; set; }
        public ClassDepartmentValidations(DBMain DbMain, DBRegistries dBRegistries)
        {
            this.DbMain = DbMain;
            this.DBRegistries = dBRegistries;
        }
        
        public async Task<bool> ValidateCreator(long CreatedById)
        {
            var creator = DbMain.Users.FirstOrDefault(s => s.Id == CreatedById && s.Deleted == 0);
            if(creator == null || creator.UserType != 3)
            {
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }
        public async Task<bool> ValidateSerialUnique(string classDepartmentserialNumber)
        {
            var schoolExists = DbMain.ClassDepartments.FirstOrDefault(s => s.SerialNumber == classDepartmentserialNumber && s.Deleted == 0);
            if (schoolExists != null)
            {
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }
        public async Task<bool> ValidateClassSerialNumber(string classDepartmentserialNumber)
        {
            if(classDepartmentserialNumber.Length < 3 || classDepartmentserialNumber.Length > 5)
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
            var schoollist = await DBRegistries.SchoolList.FirstOrDefaultAsync(s => s.Id == schoollistId);
            if (schoollist == null)
            {
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }
        public async Task<string> Validation(Models.DTOs.ClassDepartment.Create classDepartment)
        {
            validationResult = false;
            if (await ValidateCreator(classDepartment.CreatedById) == false)
            {
                return await Task.FromResult("You don't have permision to create class!");
            }
            if(await ValidateSerialUnique(classDepartment.SerialNumber) == false)
            {
                return await Task.FromResult("Class with this serial number already exists in database!");
            } 
            if (await ValidateClassSerialNumber(classDepartment.SerialNumber) == false)
            {
                return await Task.FromResult("Class serial number is invalid!");
            }

            if (await ValidateClassName(classDepartment.Name) == false)
            {
                return await Task.FromResult("Class name is invalid");
            }
            if (await ValidateSchoolListId(classDepartment.SchoolListId) == false)
            {
                return await Task.FromResult("School list ID is invalid!");
            }
            validationResult = true;
            return await Task.FromResult("School added succesfuly!");
        }
    }
}
