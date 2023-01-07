using Microsoft.EntityFrameworkCore;
using server.Database;
using server.Models.Domain;
using server.Models.DTOs.StudentGrades;
using server.Repositories.Interfaces;
using server.Services.AEmailService;

namespace server.Repositories.Classes
{
    public class StudentGradesRepository : IStudentGrades
    {
        private readonly DBMain _dbMain;
        private readonly IAEmailService _emailService;
        public StudentGradesRepository(DBMain dbMain, IAEmailService emailService)
        {
            _dbMain = dbMain;
            _emailService = emailService;
        }
        //get
        public async Task<List<GetStudentGrades>> GetGradesForStudent(long Id)
        {
            try
            {
                var query = from grades in _dbMain.StudentGrades
                            join professors in _dbMain.Users on grades.ProfessorId equals professors.Id
                            join subjects in _dbMain.Subjects on grades.SubjectId equals subjects.Id
                            where grades.StudentId == Id
                            orderby subjects.Name, grades.GradeDate 
                            select new
                            {
                                SubjectName = subjects.Name,
                                GradesDate = grades.GradeDate,
                                GradeDescription = grades.Description,
                                Grade = grades.Grade,
                                ProfessorNameAndSurname = professors.Name + " " + professors.LastName 
                            };
                if(!query.Any())
                {
                    List<GetStudentGrades> studentGrades = new();
                    foreach (var row in query)
                    {
                        GetStudentGrades grade = new()
                        {
                            SubjectName = row.SubjectName,
                            GradesDate = row.GradesDate,
                            GradeDescription = row.GradeDescription,
                            Grade = row.Grade,
                            ProfessorNameAndSurname = row.ProfessorNameAndSurname
                        };
                        studentGrades.Add(grade);
                    }
                    return await Task.FromResult(studentGrades);
                }
                throw new Exception("No grades found");
            }
            catch (Exception)
            {

                throw;
            }
        }

        //post
        public async Task<long> CreateGrade(StudentGrades grade)
        {
            try
            {
                await _dbMain.StudentGrades.AddAsync(grade);
                await _dbMain.SaveChangesAsync();
                tempEmail mail = new()
                {
                    SenderEmail = "jasim.alibegovic@outlook.com",
                    RecipientEmail = await _dbMain.Users.Where(s => s.Id == grade.StudentId).Select(s => s.Email).FirstOrDefaultAsync(),
                    RecipientId = grade.StudentId,
                    EmailHeader = "Email Header",
                    EmailText = "Email Text",
                    CreatedDate = DateTime.Today
                };
                await _emailService.PrepareEmail(mail);
                return grade.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //patch
        public async Task<long> UpdateGrade(long Id, StudentGrades grade)
        {
            try
            {
                _dbMain.StudentGrades.Update(grade);
                await _dbMain.SaveChangesAsync();
                return Id;
            }
            catch (Exception)
            {

                throw;
            }
        }
        // delete
        public async Task<long> DeleteGrade(long Id, long professorId)
        {
            try
            {
                var grade = await _dbMain.StudentGrades.FirstOrDefaultAsync(s => s.Id == Id);
                _dbMain.StudentGrades.Remove(grade);
                await _dbMain.SaveChangesAsync();
                return Id;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
