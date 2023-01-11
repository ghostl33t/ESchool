using Microsoft.EntityFrameworkCore;
using server.Database;
using server.Models.Domain;
using server.Models.DTOs.UsersDTO;
using System.Runtime.CompilerServices;

namespace server.Repositories.Classes
{
    public class UserRepository : Interfaces.IUser
    {
        private readonly DBMain _dbMain;
        public UserRepository(DBMain dbMain)
        {
            this._dbMain = dbMain;
        }
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            try
            {
                var allUsers = await _dbMain.Users.Where(s => s.Deleted == 0).ToListAsync();

                return allUsers;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<User> GetUserAsync(long id)
        {
            try
            {
                var user = await _dbMain.Users.FirstOrDefaultAsync(s => s.Id == id && s.Deleted == 0);
                return user;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public async Task<long> CreateUserAsync(User newUser)
        {
            try
            {
                newUser.CreatedDate = DateTime.Now;
                newUser.Deleted = 0;
                newUser.CreatedBy = await _dbMain.Users.FirstOrDefaultAsync(s => s.Id == newUser.CreatedBy.Id);
                await _dbMain.Users.AddAsync(newUser);
                await _dbMain.SaveChangesAsync();
                return newUser.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<long> UpdateUserAsync(long Id, User user)
        {
            //var existUser = await _dbMain.Users.FirstAsync(s => s.Id == Id);
            if(user != null)
            {
                user.Id = Id;
                //_dbMain.Entry(user).Property(x => x.Id).IsModified = false;
                _dbMain.Update(user);
                await _dbMain.SaveChangesAsync();
            }
            return user.Id;
        }
        public async Task<bool> DeleteUserAsync(long UserId, long AdministratorId)
        {
            try
            {
                var userForDelete = await _dbMain.Users.FirstOrDefaultAsync(s => s.Id == UserId);
                var Administrator = await _dbMain.Users.FirstOrDefaultAsync(s => s.Id == AdministratorId);
                if (userForDelete != null && Administrator != null)
                {
                    userForDelete.Deleted = 1;
                    userForDelete.DeletedDate = DateTime.Today;
                    userForDelete.DeletedById = AdministratorId;
                    await _dbMain.SaveChangesAsync();
                    return true;
                }
                else return false;
            }
            catch (Exception)
            {
                throw;
            } 
        }
        public async Task<float> AverageGrade(long classDepartmentId, long studentId)
        {
            var subjects = _dbMain.ClassSubjects.Include(s=>s.Subject).Where(s => s.ClassDepartment.ID == classDepartmentId).ToList(); //
            var grades = _dbMain.StudentGrades.Where(s => s.StudentId == studentId);

            List<float> avgForSubjects = new();
            int gradeCounter = 0;
            float avgGrade = 0;
            foreach (var subject in subjects)
            {
                float avgForSubject = 0;
                gradeCounter = 0;
                foreach (var grade in grades)
                {
                    if (grade.SubjectId == subject.Subject.Id)
                    {
                        avgForSubject += grade.Grade;
                        gradeCounter++;
                    }
                }
                avgForSubjects.Add(avgForSubject / gradeCounter);
            }
            foreach (var grade in avgForSubjects)
            {
                avgGrade += grade;
            }
            return avgGrade / avgForSubjects.Count;
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
                float averageGr = await AverageGrade(queryT.First().ClassDepartment.ID, Id);
                userStudentDashboard = new()
                {
                    NameAndSurname = queryT.First().Student.Name + queryT.First().Student.LastName,
                    Department = queryT.First().ClassDepartment.Name,
                    AverageGrade = String.Format("Average grade: {0}", averageGr),
                    Discipline = String.Format("Student discipline: {0}",queryT.First().StudentDiscipline),

                    BestInSubject = "Best in Subject",
                    WorstInSubject = "Worst in Subject"
                };
                return userStudentDashboard;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
