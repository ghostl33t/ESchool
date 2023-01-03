using Microsoft.EntityFrameworkCore;
using server.Database;
using server.Models.Domain;
using server.Repositories.Interfaces;

namespace server.Repositories.Classes
{
    public class StudentGradesRepository : IStudentGrades
    {
        private readonly DBMain _dbMain;
        public StudentGradesRepository(DBMain dbMain)
        {
            _dbMain = dbMain;
        }
        //get
        //post
        public async Task<long> CreateGrade(StudentGrades grade)
        {
            try
            {
                await _dbMain.StudentGrades.AddAsync(grade);
                await _dbMain.SaveChangesAsync();
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
