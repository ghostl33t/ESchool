using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using server.Database;
using server.Models.Domain;
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
