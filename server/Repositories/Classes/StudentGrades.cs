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
        public async Task<GetStudentGrades> GetGradesForStudent(long StudentId)
        {
            // pokupi predmete, profesore, vezuidmedjuistih
            var studentDetails = await DbMain.StudentsDetails.FirstOrDefaultAsync(s => s.Student.Id == StudentId);
            List<ClassDepartmentSubjectProfessor> CDSP = new List<ClassDepartmentSubjectProfessor>();
            List<Subject> Subjects = new List<Subject>();
            List<long> subjectsId = new List<long>();
            List<Subject> StudentSubjects = new List<Subject>();
            List<User> Professors = new List<User>();
            CDSP = await DbMain.ClassDepartmentSubjectProfessors.Where(s=> s.ClassDepartment.ID == studentDetails.ClassDepartment.ID).ToListAsync();
            Subjects = await DbRegistries.Subjects.ToListAsync();
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
            //napisi query;
            var query = ""
            //mapiraj podatke sa maperom a ne for petljom (lista podataka)
        }
        public Task<GetStudentGrades> GetGradesForClass(long ClassId);
        public Task<GetStudentGrades> ValidateStudentGrades(long StudentGradeId);

        private async Task<GetStudentGrades> mappedGrade (server.Models.Domain.StudentGrades src)
        {
            return mapper.Map<GetStudentGrades>(src);
        }
    }
}
