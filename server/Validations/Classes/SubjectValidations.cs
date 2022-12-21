using Microsoft.EntityFrameworkCore;
using server.Database;
using server.Validations.Interfaces;

namespace server.Validations.Classes
{
    public class SubjectValidations : ISubjectValidations
    {
        private readonly DBMain DbMain;
        private readonly DBRegistries DBRegistries;
        
        public bool validationResult { get; set; }
        public int code { get; set; }
        public SubjectValidations(DBMain DbMain, DBRegistries dBRegistries)
        {
            this.DbMain = DbMain;
            this.DBRegistries = dBRegistries;
        }

        public async Task<bool> ValidateCreator(long CreatedById)
        {
            var creator = DbMain.Users.FirstOrDefault(s => s.Id == CreatedById && s.Deleted == 0);
            if (creator == null || creator.UserType != 0)
            {
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }
        public async Task<bool> ValidateSerialUnique(string serialNumber)
        {
            var subjectExist = DBRegistries.Subjects.FirstOrDefault(s => s.SerialNumber == serialNumber && s.Deleted == 0);
            if (subjectExist != null)
            {
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }
        public async Task<bool> ValidateSerialNumberLength(string serialNumber)
        {
            if (serialNumber.Length < 3 || serialNumber.Length > 5)
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
        public async Task<string> Validation(Models.DTOs.Subject.Create subject)
        {
            validationResult = false;
            
            if (await ValidateCreator(subject.CreatedById) == false)
            {
                code = 401;
                return await Task.FromResult("You don't have permision to create subject!");
            }
            if (await ValidateSerialUnique(subject.SerialNumber) == false)
            {
                code = 400;
                return await Task.FromResult("Subject with this serial number already exists in database!");
            }
            if (await ValidateSerialNumberLength(subject.SerialNumber) == false)
            {
                code = 400;
                return await Task.FromResult("Subject serial number is invalid!");
            }
            if (await ValidateSubjectName(subject.Name) == false)
            {
                code = 400;
                return await Task.FromResult("Subject name is invalid");
            }
            code = 201;
            validationResult = true;
            return await Task.FromResult("Subject added succesfuly!");
        }
        public async Task<string> Validation(Models.DTOs.Subject.Update subject)
        {
            validationResult = false;
            var subjectExist = await this.DBRegistries.Subjects.FirstOrDefaultAsync(s => s.Id == subject.Id);
            if (await ValidateCreator(subject.UpdatedById) == false)
            {
                code = 401;
                return await Task.FromResult("You don't have permision to create subject!");
            }
            if(subjectExist.SerialNumber != subject.SerialNumber)
            {
                if (await ValidateSerialUnique(subject.SerialNumber) == false)
                {
                    code = 400;
                    return await Task.FromResult("Subject with this serial number already exists in database!");
                }
                if (await ValidateSerialNumberLength(subject.SerialNumber) == false)
                {
                    code = 400;
                    return await Task.FromResult("Subject serial number is invalid!");
                }
            }
            if(subjectExist.Name != subject.Name)
            {
                if (await ValidateSubjectName(subject.Name) == false)
                {
                    code = 400;
                    return await Task.FromResult("Subject name is invalid");
                }
            }
            code = 204;
            validationResult = true;
            return await Task.FromResult("Subject added succesfuly!");
        }
        public async Task<string> Validation(long SubjectId, long AdministratorId)
        {
            var subject = await this.DBRegistries.Subjects.FirstOrDefaultAsync(s => s.Id == SubjectId);
            var Administrator = await this.DbMain.Users.FirstOrDefaultAsync(s => s.Id == AdministratorId);
            if(Administrator != null)
            {
                if (Administrator.UserType != 0)
                {
                    code = 401;
                    return await Task.FromResult("Unauthorized!");
                }
                if(subject == null)
                {
                    code = 400;
                    return await Task.FromResult("Subject not found!");
                }
            }
            validationResult = true;
            code = 204;
            return await Task.FromResult("Subject deleted succesfuly!");
            
        }
    }
}
