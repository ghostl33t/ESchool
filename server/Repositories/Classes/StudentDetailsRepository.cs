using Microsoft.EntityFrameworkCore;
using server.Database;
using server.Models.Domain;
using server.Repositories.Interfaces;

namespace server.Repositories.Classes
{
    public class StudentDetailsRepository : IStudentDetails
    {
        private readonly DBMain _dbMain;
        public StudentDetailsRepository(DBMain dbmain)
        {
            _dbMain = dbmain;
        }
        public async Task<bool> CreateStudentDetails(StudentDetails studentdet)
        {
            try
            {
                var student = await _dbMain.Users.FirstOrDefaultAsync(s => s.Id == studentdet.StudId);
                studentdet.Student = student;
                studentdet.ClassDepartment = await _dbMain.ClassDepartments.FirstOrDefaultAsync(s => s.ID == studentdet.ClassDepId);
                await _dbMain.StudentsDetails.AddAsync(studentdet);
                await _dbMain.SaveChangesAsync();
                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
