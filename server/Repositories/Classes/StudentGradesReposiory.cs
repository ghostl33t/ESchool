using Microsoft.EntityFrameworkCore;
using server.Database;
using server.Repositories.Interfaces;
using server.Models.Domain;
using server.Models.DTOs.StudentGrades;

namespace server.Repositories.Classes;
    public class StudentGradesReposiory : IStudentGrades
    {
        private readonly DBMain _dbMain;
        private readonly DBRegistries _dbRegistries;
        public StudentGradesReposiory(DBMain dbMain, DBRegistries dbRegistries)
        {
            _dbMain = dbMain;
            _dbRegistries = dbRegistries;
        }
        public async Task<long> CreateGradeAsync(StudentGrades grade)
        {
            try
            {
                await _dbMain.StudentsGrades.AddAsync(grade);
                await _dbMain.SaveChangesAsync();
                return grade.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<long> UpdateGradeAsync(StudentGrades grade)
        {
            try
            {
            _dbMain.StudentsGrades.Update(grade);
            await _dbMain.SaveChangesAsync();
            return grade.Id; 
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public async Task<long> DeleteGradeAsync(long Id, long deletedbyid)
        {
            try
            {
                var grade = await _dbMain.StudentsGrades.FirstAsync(s => s.Id == Id);
                grade.Deleted = 1;
                grade.DeletedDate = DateTime.Today;
                grade.DeletedById = deletedbyid;
                await _dbMain.SaveChangesAsync();
                return Id;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<GetStudentGrades>> GetGradesForStudent(long StudentId)
        {
            //List obj
            List<server.Models.Domain.StudentGrades> studentGrades = await _dbMain.StudentsGrades.Where(s => s.Student.Id == StudentId && s.Deleted == 0).ToListAsync();
            List<ClassDepartmentSubjectProfessor> CDSP = new List<ClassDepartmentSubjectProfessor>();
            List<Subject> Subjects = new List<Subject>();
            List<long> subjectsId = new List<long>();
            List<Subject> StudentSubjects = new List<Subject>();
            List<GetStudentGrades> StudentGradesInfo = new List<GetStudentGrades>();
            List<server.Models.Domain.User> Professors = await _dbMain.Users.Where(s => s.Deleted == 0 && s.UserType == 1).ToListAsync();
            // assign value
            var studentDetails = await _dbMain.StudentsDetails.FirstOrDefaultAsync(s => s.Student.Id == StudentId && s.Deleted == 0);            // assing value
            CDSP = await _dbMain.ClassDepartmentSubjectProfessors.Where(s=> s.ClassDepartment.ID == studentDetails.ClassDepartment.ID && s.Deleted == 0).ToListAsync();
            Subjects = await _dbRegistries.Subjects.Where(s=> s.Deleted == 0).ToListAsync();
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
        //public Task<GetStudentGrades> GetGradesForClass(long ClassId)
        //{
        //    return null;
        //}
        //public Task<GetStudentGrades> ValidateStudentGrades(long StudentGradeId)
        //{
        //    return null;
        //}

        //private async Task<GetStudentGrades> mappedGrade (server.Models.Domain.StudentGrades src)
        //{
        //    return mapper.Map<GetStudentGrades>(src);
        //}
    }

