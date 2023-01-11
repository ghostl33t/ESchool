using Microsoft.EntityFrameworkCore;
using server.Database;
using server.Models.Domain;
using server.Models.DTOs.StudentGrades;
using server.Repositories.Interfaces;
using server.Services.AEmailService;
using System.Data;

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

        //average grade for student
        public async Task<float> AverageGrade(long classDepartmentId, long studentId)
        {
            var subjects = _dbMain.ClassSubjects.Include(s => s.Subject).Where(s => s.ClassDepartment.ID == classDepartmentId).ToList(); //
            var grades = _dbMain.StudentGrades.Where(s => s.StudentId == studentId);

            float[] avgForSubjects = new float[subjects.Count];
            int gradeCounter = 0;
            float avgGrade = 0;
            foreach (var item in subjects.Select((value,i) => new { i, value }))
            {
                float avgForSubject = 0;
                gradeCounter = 0;
                foreach (var grade in grades)
                {
                    if (grade.SubjectId == item.value.Subject.Id)
                    {
                        avgForSubject += grade.Grade;
                        gradeCounter++;
                    }
                }
                avgForSubjects[item.i] = avgForSubject / gradeCounter;
            }
            foreach (var grade in avgForSubjects)
            {
                avgGrade += grade;
            }
            float result = avgGrade / avgForSubjects.Length;
            return await Task.FromResult(result);
        }
        //best worst grades 
        public async Task<float[,]> BestWorstSubjectGrade(long classDepartmentId, long studentId)
        {
            var subjects = _dbMain.ClassSubjects.Include(s => s.Subject).Where(s => s.ClassDepartment.ID == classDepartmentId).ToList(); //
            var grades = _dbMain.StudentGrades.Where(s => s.StudentId == studentId);
            int gradeCounter = 0;
            //0 -> Grade ; 1 -> SujectId
            float[,] tmpResult = new float[subjects.Count,2];
            foreach (var item in subjects.Select((value, i) => new { i, value }))
            {
                float avgForSubject = 0;
                gradeCounter = 0;
                foreach (var grade in grades)
                {
                    if (grade.SubjectId == item.value.Subject.Id)
                    {
                        avgForSubject += grade.Grade;
                        gradeCounter++;
                    }
                }
                tmpResult[item.i,0] =  (avgForSubject / gradeCounter) ;
                tmpResult[item.i, 1] = item.value.Subject.Id;
            }
            if(tmpResult.GetLength(0) > 1)
            {
                for (int i = 0; i < (subjects.Count - 1) - 1; i++)
                {
                    for (int j = 0; j < (subjects.Count - 1) - 1 - i; j++)
                    {
                        if (tmpResult[j, 0] < tmpResult[j + 1, 0])
                        {
                            float[,] tempValue = new float[1, 2];
                            tempValue[0, 0] = tmpResult[j, 0];
                            tempValue[0, 1] = tmpResult[j, 1];

                            tmpResult[j, 0] = tmpResult[j + 1, 0];
                            tmpResult[j, 0] = tmpResult[j + 1, 1];

                            tmpResult[j + 1, 0] = tempValue[0, 0];
                            tmpResult[j + 1, 1] = tempValue[0, 1];
                        }
                    }
                }
            }
            
            float[,] result = new float[2, 2] { { 0,0}, { 0,0} };

            result[0, 0] = tmpResult[0, 0];
            result[0, 1] = tmpResult[0, 1];
            if(tmpResult.GetLength(0) > 1)
            {
                result[1, 0] = tmpResult[1, 0];
                result[1, 1] = tmpResult[1, 1];
            }
            
            return await Task.FromResult(result);

        }
    }
}
