using Microsoft.EntityFrameworkCore;
using server.Database;

namespace server.Validations
{
    public class SubjectValidations : ISubjectValidations
    {
        private readonly DBMain DbMain;
        private readonly DBRegistries DBRegistries;
        public bool validationResult { get; set; }
        public SubjectValidations(DBMain DbMain, DBRegistries dBRegistries)
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
        public async Task<bool> ValidateSerialUnique(string serialNumber)
        {
            var subjectExist = this.DBRegistries.Subjects.FirstOrDefault(s => s.SerialNumber == serialNumber && s.Deleted == 0);
            if (subjectExist != null)
            {
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }
        public async Task<bool> ValidateSerialNumberLength(string serialNumber)
        {
            if(serialNumber.Length < 3 || serialNumber.Length > 5)
            {
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }
        public async Task<bool> ValidateSubjectName(string name)
        {
            if (name.Length < 5 || name.Length > 15)
            {
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }
        //public async Task<bool> ValidateSchoolListId(long schoollistId) //TODO ovdje kontrolisati da li tip skole postoji u registrima
        //{
        //    var schoollist = await DBRegistries.SchoolList.FirstOrDefaultAsync(s => s.Id == schoollistId);
        //    if (schoollist == null)
        //    {
        //        return await Task.FromResult(false);
        //    }
        //    return await Task.FromResult(true);
        //}
        public async Task<string> Validation(Models.DTOs.Subject.Create subject)
        {
            validationResult = false;
            if (await ValidateCreator(subject.CreatedById) == false)
            {
                return await Task.FromResult("You don't have permision to create class!");
            }
            if(await ValidateSerialUnique(subject.SerialNumber) == false)
            {
                return await Task.FromResult("Class with this serial number already exists in database!");
            } 
            if (await ValidateSerialNumberLength(subject.SerialNumber) == false)
            {
                return await Task.FromResult("Class serial number is invalid!");
            }

            if (await ValidateSubjectName(subject.Name) == false)
            {
                return await Task.FromResult("Class name is invalid");
            }
            //if (await ValidateSchoolListId(classDepartment.SchoolListId) == false)
            //{
            //    return await Task.FromResult("School list ID is invalid!");
            //}
            validationResult = true;
            return await Task.FromResult("School added succesfuly!");
        }
    }
}
