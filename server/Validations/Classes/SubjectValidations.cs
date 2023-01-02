using Microsoft.EntityFrameworkCore;
using server.Database;
using server.Validations.Interfaces;

namespace server.Validations.Classes
{
    public class SubjectValidations : ISubjectValidations
    {
        private readonly DBMain _dbMain;

        public string validationMessage { get; set; } = String.Empty;
        public int code { get; set; }
        public SubjectValidations(DBMain dbMain )
        {
            this._dbMain = dbMain;
        }

        public async Task<bool> ValidateCreator(long CreatedById)
        {
            var creator = _dbMain.Users.AsNoTracking().FirstOrDefault(s => s.Id == CreatedById && s.Deleted == 0);
            if (creator == null || creator.UserType != 0)
            {
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }
        public async Task<bool> ValidateSerialUnique(string serialNumber)
        {
            var subjectExist = _dbMain.Subjects.AsNoTracking().FirstOrDefault(s => s.SerialNumber == serialNumber && s.Deleted == 0);
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
        public async Task<bool> Validation(Models.DTOs.Subject.PostSubject subject)
        {
            code = 0;
            
            if (await ValidateCreator(subject.CreatedById) == false)
            {
                code = 401;
                validationMessage =  "You don't have permision to create subject!";
            }
            if (await ValidateSerialUnique(subject.SerialNumber) == false)
            {
                code = 400;
                validationMessage = "Subject with this serial number already exists in database!";
            }
            if (await ValidateSerialNumberLength(subject.SerialNumber) == false)
            {
                code = 400;
                validationMessage = "Subject serial number is invalid!";
            }
            if (await ValidateSubjectName(subject.Name) == false)
            {
                code = 400;
                validationMessage = "Subject name is invalid";
            }
            if(code !=0) { return false; }
            code = 201;
            validationMessage = await Task.FromResult("Subject added successfuly!");
            return true;
        }
        public async Task<bool> Validation(long Id,Models.DTOs.Subject.PatchSubject subject)
        {
            code = 0;
            var subjectExist = await this._dbMain.Subjects.AsNoTracking().FirstOrDefaultAsync(s => s.Id == Id);
            if (await ValidateCreator(subject.UpdatedById) == false)
            {
                code = 401;
                validationMessage = "You don't have permision to create subject!";
            }
            if (subjectExist != null)
            {
                if (subjectExist.SerialNumber != subject.SerialNumber)
                {
                    if (await ValidateSerialUnique(subject.SerialNumber) == false)
                    {
                        code = 400;
                        validationMessage = "Subject with this serial number already exists in database!";
                    }
                    if (await ValidateSerialNumberLength(subject.SerialNumber) == false)
                    {
                        code = 400;
                        validationMessage = "Subject serial number is invalid!";
                    }
                }
                if (subjectExist.Name != subject.Name)
                {
                    if (await ValidateSubjectName(subject.Name) == false)
                    {
                        code = 400;
                        validationMessage = "Subject name is invalid";
                    }
                }
            }
            if (code != 0) { return false; }
            code = 204;
            validationMessage = "Subject updated successfuly!";
            return true;
        }
        public async Task<bool> Validation(long SubjectId, long AdministratorId)
        {
            var subject = await _dbMain.Subjects.AsNoTracking().FirstOrDefaultAsync(s => s.Id == SubjectId);
            var Administrator = await _dbMain.Users.AsNoTracking().FirstOrDefaultAsync(s => s.Id == AdministratorId);
            if(Administrator != null)
            {
                if (Administrator.UserType != 0)
                {
                    code = 401;
                    validationMessage = "Unauthorized!";
                }
                if(subject == null)
                {
                    code = 400;
                    validationMessage = "Subject not found!";
                }
            }
            if(code != 0) { return false; }
            code = 204;
            validationMessage = "Subject deleted successfuly!";
            return true;
        }
    }
}
