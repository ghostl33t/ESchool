using server.Models.Domain;
using server.Models.DTOs.StudentGrades;

namespace server.Repositories.Interfaces
{
    public interface IStudentGrades
    {
        public Task<long> CreateGradeAsync(StudentGrades grade);
        public Task<long> UpdateGradeAsync(StudentGrades grade);
        public Task<long> DeleteGradeAsync(long Id, long deletedbyid);
        public Task<List<GetStudentGrades>> GetGradesForStudent(long StudentId);
        //TODO
        //public Task<GetStudentGrades> GetGradesForClass(long ClassId);
        //public Task<GetStudentGrades> ValidateStudentGrades(long StudentGradeId);


    }
}
