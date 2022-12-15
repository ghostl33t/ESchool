using AutoMapper;
using Microsoft.EntityFrameworkCore;
using server.Database;
using server.Models.DTOs.StudentGrades;
using server.Repositories.Interfaces;
using server.Validations;
using System.Diagnostics;
using server.Models.Domain;
using server.Controllers;

namespace server.Repositories.Classes
{
    public class StudentGrades : IStudentGrades
    {
        private readonly DBMain DbMain;
        private readonly DBRegistries DbRegistries;
        private readonly IMapper mapper;
        //TODO OVO U KONTROLER private readonly IStudentGradesValidations StudentGradesValidation;
        public StudentGrades(DBMain dbMain, DBRegistries dbRegistries, IMapper mapper)
        {
            DbMain = dbMain;
            DbRegistries = dbRegistries;
            this.mapper = mapper;
        }
    
        public async Task<GetStudentGrades> CreateGradeAsync(Create create)
        {
            try
            {
                create.ClassDepartmentSubjectProfessor = await this.DbMain.ClassDepartmentSubjectProfessors.FirstOrDefaultAsync(s => s.Id == create.ClassDepartmentSubjectProfessorId);
                create.Student = await this.DbMain.Users.FirstOrDefaultAsync(s => s.Id == create.StudentId);
                var grade = mapper.Map<server.Models.Domain.StudentGrades>(create);
                await DbMain.AddAsync(grade);
                await DbMain.SaveChangesAsync();
                return await mappedGrade(grade);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<GetStudentGrades> UpdateGradeAsync(long id, Update update)
        {
            try
            {
                var grade = await DbMain.StudentsGrades.FirstOrDefaultAsync(s => s.Id == id);
                if (grade != null)
                {
                    grade.Student = update.UserStudent;
                    grade.Grade = update.Grade;
                    grade.Description = update.Description;
                    grade.Validated = update.Validated;
                    grade.ClassDepartmentSubjectProfessor = update.ClassDepartmentSubjectProfessor;
                    await DbMain.SaveChangesAsync();
                }
                return await mappedGrade(grade);
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
        public async Task<GetStudentGrades> DeleteGradeAsync(long Id, long deletedbyid)
        {
            try
            {
                var grade = await DbMain.StudentsGrades.FirstOrDefaultAsync(s => s.Id == Id);
                grade.Deleted = 1;
                grade.DeletedDate = DateTime.Today;
                grade.DeletedById = deletedbyid;
                await DbMain.SaveChangesAsync();
                await DbMain.SaveChangesAsync();
                return await mappedGrade(grade);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<List<GetStudentGrades>> GetGradesForStudent(long StudentId)
        {
            //List obj
            List<server.Models.Domain.StudentGrades> studentGrades = await DbMain.StudentsGrades.Where(s => s.Student.Id == StudentId && s.Deleted == 0).ToListAsync();
            List<ClassDepartmentSubjectProfessor> CDSP = new List<ClassDepartmentSubjectProfessor>();
            List<Subject> Subjects = new List<Subject>();
            List<long> subjectsId = new List<long>();
            List<Subject> StudentSubjects = new List<Subject>();
            List<GetStudentGrades> StudentGradesInfo = new List<GetStudentGrades>();
            List<server.Models.Domain.User> Professors = await DbMain.Users.Where(s => s.Deleted == 0 && s.UserType == 1).ToListAsync();
            // assign value
            var studentDetails = await DbMain.StudentsDetails.FirstOrDefaultAsync(s => s.Student.Id == StudentId && s.Deleted == 0);            // assing value
            CDSP = await DbMain.ClassDepartmentSubjectProfessors.Where(s=> s.ClassDepartment.ID == studentDetails.ClassDepartment.ID && s.Deleted == 0).ToListAsync();
            Subjects = await DbRegistries.Subjects.Where(s=> s.Deleted == 0).ToListAsync();
            foreach(var cdspst in CDSP)
            {
                long tmpsubjid = cdspst.SubjectID;
                subjectsId.Add(tmpsubjid);
            }
            var queryforSubjects = from subjects in Subjects
                                   where subjectsId.Contains(subjects.Id)
                                   select subjects;
            foreach(var subject in queryforSubjects)
            {
                StudentSubjects.Add(subject);
            }

            var query = from sg in studentGrades
                        join sb in StudentSubjects on sg.Id equals sb.Id
                        join cdsp in CDSP on sg.ClassDepartmentSubjectProfessor.Id equals cdsp.Id
                        join prof in Professors on sg.ClassDepartmentSubjectProfessor.UserProfessor.Id equals prof.Id
                        orderby sb.Name, sg.CreatedDate descending
                        select new
                        {
                            Subject = sb.Name,
                            Grade = sg.Grade,
                            Description = sg.Description,
                            ProfessorNameAndSurname = prof.Name + " " + prof.LastName,
                            GradeDate = sg.CreatedDate,
                            Verified = (
                                sg.Validated == 1 ? "YES" :
                                sg.Validated == 0 ? "NO" : "Undefined"
                            )
                            
                        };
            foreach(var row in query)
            {
                GetStudentGrades newGrade = new GetStudentGrades();
                newGrade.StudentNameAndSurname = studentDetails.Student.Name + " " + studentDetails.Student.LastName;
                newGrade.Subject = row.Subject;
                newGrade.GradeDate = row.GradeDate;
                newGrade.Grade = row.Grade;
                newGrade.ProfessorNameAndSurname = row.ProfessorNameAndSurname;
                newGrade.Verified = row.Verified;
                StudentGradesInfo.Add(newGrade);
            }
            return StudentGradesInfo;
        }
        public Task<GetStudentGrades> GetGradesForClass(long ClassId)
        {
            return null;
        }
        public Task<GetStudentGrades> ValidateStudentGrades(long StudentGradeId)
        {
            return null;
        }

        private async Task<GetStudentGrades> mappedGrade (server.Models.Domain.StudentGrades src)
        {
            return mapper.Map<GetStudentGrades>(src);
        }
    }
}
