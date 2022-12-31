using Microsoft.EntityFrameworkCore;
using server.Database;
using server.Models.Domain;
using server.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace server.Repositories.Classes
{
    public class StudentDetailsRepository : IStudentDetails
    {
        private readonly DBMain _dbMain;
        public StudentDetailsRepository(DBMain dbmain)
        {
            _dbMain = dbmain;
        }
        public async Task<long> CreateStudentDetails(StudentDetails studentdet)
        {
            try
            {
                var student = await _dbMain.Users.FirstOrDefaultAsync(s => s.Id == studentdet.StudId);
                studentdet.Student = student;
                studentdet.ClassDepartment = await _dbMain.ClassDepartments.FirstOrDefaultAsync(s => s.ID == studentdet.ClassDepId);
                await _dbMain.StudentsDetails.AddAsync(studentdet);
                await _dbMain.SaveChangesAsync();
                return studentdet.Id;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<long> UpdateStudentDetails( StudentDetails studentdet)
        {
            try
            {
                if(studentdet != null)
                {
                    _dbMain.StudentsDetails.Update(studentdet);
                    await _dbMain.SaveChangesAsync();
                }
                return studentdet.Id;
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
                var validadmin = await _dbMain.Users.AsNoTracking().FirstOrDefaultAsync(s => s.Id == AdministratorId && s.Deleted == 0 && s.UserType == 0);
                if(validadmin != null)
                {
                    var studentDetails = await _dbMain.StudentsDetails.AsNoTracking().FirstOrDefaultAsync(s => s.Id == Id);
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
    }
}
