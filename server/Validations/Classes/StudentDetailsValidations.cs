using Microsoft.EntityFrameworkCore;
using server.Database;
using server.Models.Domain;
using server.Models.DTOs.StudentDetails;
using server.Repositories.Interfaces;
using server.Validations.Interfaces;

namespace server.Validations.Classes
{
    public class StudentDetailsValidations : IStudentDetailsValidations
    {
        public int code { get; set; }
        public string validationMessage { get; set; } = string.Empty;
        private readonly DBMain _dbMain;
        private readonly DBRegistries _dbRegistries;

        public StudentDetailsValidations(DBMain dbMain, DBRegistries dbRegistries)
        {
            _dbMain = dbMain;
            _dbRegistries = dbRegistries;
        }
        //Validiranje da li je kreator administrator
        public async Task<bool> ValidateCreator(long creatorId)
        {
            var creator = await _dbMain.Users.AsNoTracking().FirstOrDefaultAsync(s => s.Id == creatorId && s.Deleted == 0);
            if (creator == null || creator.UserType != 0)
            {
                return false;
            }
            return true;
        }
        //Validiranje da li postoji student
        public async Task<bool> ValidateStudent(long studId)
        {
            var student = await _dbMain.Users.AsNoTracking().FirstOrDefaultAsync(s => s.Id == studId && s.Deleted == 0);
            if (student == null || student.UserType != 2)
            {
                return false;
            }
            return true;
        }
        //Validiranje da li postoji ClassDepartment
        public async Task<bool> ValidateClassDepartment(long cdId)
        {
            var classdep = await _dbMain.ClassDepartments.AsNoTracking().FirstOrDefaultAsync(s => s.ID == cdId && s.Deleted == 0);
            if (classdep == null)
            {
                return false;
            }
            return true;
        }
        //Validiranje roditelja 1 i 2 samo pozivat 2 put metode 
        public async Task<bool> ValidateParent(long parentId)
        {
            var parent = await _dbMain.Users.AsNoTracking().FirstOrDefaultAsync(s => s.Id == parentId && s.Deleted == 0);
            if (parent == null || parent.UserType != 3)
            {
                return false;
            }
            return true;
        }
        //Validiranje discipline od 1 do 5 
        public async Task<bool> ValidateDiscipline(int? discipline)
        {
            if(discipline < 1 || discipline > 5) { return false; }
            return await Task.FromResult(true);
        }

        public async Task<bool> Validate(PostStudentDetails studentDetails)
        {
            code = 0;
            if(await ValidateCreator(studentDetails.CreatedById) == false)
            {
                code = 401;
                validationMessage = "Unauthorized!";
            }
            if(await ValidateStudent(studentDetails.StudentId_) == false)
            {
                code = 400;
                validationMessage = "Invalid student Id !";
            }
            if(await ValidateClassDepartment(studentDetails.ClassDepartmentId_) == false)
            {
                code = 400;
                validationMessage = "Class department is not defined";
            }
            if(studentDetails.ParentId1 != 0 || studentDetails.ParentId2 != 0)
            {
                if (await ValidateParent(studentDetails.ParentId1) == false && await ValidateParent(studentDetails.ParentId2) == false)
                {
                    code = 400;
                    validationMessage = "Parent is not defined";
                }
            }
            else
            {
                code = 400;
                validationMessage = "Parent is not defined";
            }
            if (await ValidateDiscipline(studentDetails.StudentDiscipline) == false)
            {
                code = 400;
                validationMessage = "Incorrect student discipline!";
            }
            if (code != 0) { return false; }
            code = 201;
            validationMessage = "Student details created successfuly";
            return true;
        }
        public async Task<bool> Validate(long studentDetailsId, PatchStudentDetails studentDetails)
        {
            code = 0;
            if(await _dbMain.StudentsDetails.AsNoTracking().FirstOrDefaultAsync(s => s.Id == studentDetailsId) == null)
            {
                code = 400;
                validationMessage = "Student details does not exists in database !";
            }
            if (await ValidateCreator(studentDetails.UpdatedById) == false)
            {
                code = 401;
                validationMessage = "Unauthorized!";
            }
            if (await ValidateStudent(studentDetails.StudentId_) == false)
            {
                code = 400;
                validationMessage = "Invalid student Id !";
            }
            if (await ValidateClassDepartment(studentDetails.ClassDepartmentId_) == false)
            {
                code = 400;
                validationMessage = "Class department is not defined";
            }
            if (studentDetails.ParentId1 != 0 || studentDetails.ParentId2 != 0)
            {
                if (await ValidateParent(studentDetails.ParentId1) == false && await ValidateParent(studentDetails.ParentId1) == false)
                {
                    code = 400;
                    validationMessage = "Parent is not defined";
                }
            }
            else
            {
                code = 400;
                validationMessage = "Parent is not defined";
            }
            if (await ValidateDiscipline(studentDetails.StudentDiscipline) == false)
            {
                code = 400;
                validationMessage = "Incorrect student discipline!";
            }
            if (code != 0) { return false; }
            code = 200;
            validationMessage = "Student details updated successfuly";
            return true;
        }
        public async Task<bool> Validate(long studentDetailsId, long administratorId)
        {
            code = 0;
            if (await ValidateCreator(administratorId) == false)
            {
                code = 401;
                validationMessage = "Unauthorized!";
            }
            if (await _dbMain.StudentsDetails.AsNoTracking().FirstOrDefaultAsync(s => s.Id == studentDetailsId) == null)
            {
                code = 400;
                validationMessage = "Student details does not exists in database !";
            }
            if (code != 0) { return false; }
            code = 201;
            validationMessage = "Student details deleted succesfuly";
            return true;
        }




    }
}
