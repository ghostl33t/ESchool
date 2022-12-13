using Microsoft.EntityFrameworkCore;
using server.Database;
using server.Models.Domain;

namespace server.Validations
{
    public class StudentGradesValidations : IStudentGradesValidations
    {
        public bool validationResult { get; set; }
        private readonly DBMain DbMain;
        private readonly DBRegistries DbRegistries;
        public StudentGradesValidations(DBMain DbMain, DBRegistries DbRegistries)
        {
            this.DbMain = DbMain;
            this.DbRegistries = DbRegistries;
        }
        public async Task<bool> StudentExist(long studentId)
        {
            var studentExist = await this.DbMain.Users.FirstOrDefaultAsync(s => s.Id == studentId && s.UserType == 2);
            if (studentExist != null)
                return true;
            return false;
        }
        public async Task<bool> StudentSubject(long studentid, long subjectId)
        {
            var studentDetails = await this.DbMain.StudentsDetails.FirstOrDefaultAsync(s => s.Student.Id == studentid);
            if (studentDetails == null)
            {
                return false;
            }
            var studentHaveSubject = await this.DbMain.ClassDepartmentSubjectProfessors.FirstOrDefaultAsync(s => s.SubjectID == subjectId && s.ClassDepartment.ID == studentDetails.ClassDepartment.ID);
            if (studentHaveSubject != null)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> ValidateProfessor(long studentid, long professorId)
        {
            var studentDetails = await this.DbMain.StudentsDetails.FirstOrDefaultAsync(s => s.Student.Id == studentid);
            var validProfessor = await this.DbMain.ClassDepartmentSubjectProfessors.FirstOrDefaultAsync(s => s.UserProfessor.Id == professorId && s.ClassDepartment.ID == studentDetails.ClassDepartment.ID);
            if (validProfessor != null)
            {
                return true;
            }
            return false;
        }
        public async Task<string> Validations(server.Models.DTOs.StudentGrades.Create create) //TODO Model koji ce se prosljedjivati ovisno da li je Create/Update
        {
            string message = "";
            if( await StudentExist(create.UserStudent.Id) == false)
            {
                message = "Student is not valid!";
                validationResult = false;
            }
            else if (await StudentSubject(create.UserStudent.Id, create.ClassDepartmentSubjectProfessor.SubjectID) == false){
                message = "Student doesn't have this subject!";
                validationResult = false;
            }
            else if (await ValidateProfessor(create.UserStudent.Id, create.ClassDepartmentSubjectProfessor.UserProfessor.Id) == false)
            {
                message = "Professor is invalid!";
                validationResult = false;
            }
            if (message != "")
                return message;
            else
            {
                validationResult = true;
                message = "Grade to student added succesfuly!";
            }
            return message;
        }
    }
}
