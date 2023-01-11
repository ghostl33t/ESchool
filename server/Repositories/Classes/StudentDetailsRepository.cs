using Microsoft.EntityFrameworkCore;
using MimeKit.Encodings;
using server.Database;
using server.Models.Domain;
using server.Models.DTOs.UsersDTO;
using server.Repositories.Interfaces;
namespace server.Repositories.Classes
{
    public class StudentDetailsRepository : IStudentDetails
    {
        private readonly DBMain _dbMain;
        private readonly IStudentGrades _studentGradesRepo;
        public StudentDetailsRepository(DBMain dbmain, IStudentGrades studentGradesRepo)
        {
            _dbMain = dbmain;
            _studentGradesRepo = studentGradesRepo;
        }
        public async Task<long> CreateStudentDetails(StudentDetails studentdet)
        {
            try
            {
                var student = await _dbMain.Users.FirstOrDefaultAsync(s => s.Id == studentdet.StudentId_);
                studentdet.Student = student;
                studentdet.ClassDepartment = await _dbMain.ClassDepartments.FirstOrDefaultAsync(s => s.ID == studentdet.ClassDepartmentId_);
                await _dbMain.StudentsDetails.AddAsync(studentdet);
                await _dbMain.SaveChangesAsync();
                return studentdet.Id;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<long> UpdateStudentDetails(long Id, StudentDetails studentdet)
        {
            try
            {
                if(studentdet != null)
                {
                    studentdet.Id = Id;
                    studentdet.ClassDepartment = await _dbMain.ClassDepartments.FirstOrDefaultAsync(s => s.ID == studentdet.ClassDepartmentId_);
                    _dbMain.StudentsDetails.Update(studentdet);
                    await _dbMain.SaveChangesAsync();
                }
                return Id;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<long> DeleteStudentDetails(long Id, long AdministratorId)
        {
            try
            {
                var validadmin = await _dbMain.Users.AsNoTracking().FirstOrDefaultAsync(s => s.Id == AdministratorId && s.Deleted == 0 && s.UserType == UserType.Administrator);
                if(validadmin != null)
                {
                    var studentDetails = await _dbMain.StudentsDetails.FirstOrDefaultAsync(s => s.Id == Id);
                    studentDetails.Deleted = 1;
                    studentDetails.DeletedDate = DateTime.Now;
                    studentDetails.DeletedById = AdministratorId;
                    _dbMain.StudentsDetails.Update(studentDetails);
                    await _dbMain.SaveChangesAsync();
                    return studentDetails.Id;
                }
                return 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<UserStudentDashboard> GetUserStudentDashboard(long Id)
        {

            try
            {
                var queryT = _dbMain.StudentsDetails
                    .Include(s => s.Student)
                    .Include(s => s.ClassDepartment)
                    .Where(s => s.Student.Id == Id);
                UserStudentDashboard userStudentDashboard = new();
                float averageGr = await _studentGradesRepo.AverageGrade(queryT.First().ClassDepartment.ID, Id);
                float[,] bestWorstGr = await _studentGradesRepo.BestWorstSubjectGrade(queryT.First().ClassDepartment.ID, Id);
                userStudentDashboard = new()
                {
                    NameAndSurname = queryT.First().Student.Name + queryT.First().Student.LastName,
                    Department = queryT.First().ClassDepartment.Name,
                    AverageGrade = String.Format("Average grade: {0}", averageGr),
                    Discipline = String.Format("Student discipline: {0}", queryT.First().StudentDiscipline),

                    BestInSubject = String.Format("Best in Subject: \nSubject:{0}\nGrade:{1}", _dbMain.Subjects.Where(s=>s.Id == Convert.ToInt64(bestWorstGr[0,1])).FirstOrDefault().Name ,bestWorstGr[0,0]),
                    
                };
                if (bestWorstGr[1,0] != 0)
                {
                    userStudentDashboard.WorstInSubject = String.Format("Worst in Subject: \nSubject:{0}\nGrade:{1}", _dbMain.Subjects.Where(s => s.Id == Convert.ToInt64(bestWorstGr[1, 1])).FirstOrDefault().Name, bestWorstGr[1, 0]);
                }
                else
                {
                    userStudentDashboard.WorstInSubject = "YDB";
                }
                return userStudentDashboard;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
