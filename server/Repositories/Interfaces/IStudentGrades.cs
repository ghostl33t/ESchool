using server.Models.DTOs.StudentGrades;

namespace server.Repositories.Interfaces
{
    public interface IStudentGrades
    {
        public Task<GetStudentGrades> CreateGradeAsync(Create create);
        public Task<GetStudentGrades> UpdateGradeAsync(long id, Update update);
        public Task<GetStudentGrades> DeleteGradeAsync(long Id, long deletedbyid);
        public Task<GetStudentGrades> GetGradesForStudent(long StudentId);
        public Task<GetStudentGrades> GetGradesForClass(long ClassId);
        public Task<GetStudentGrades> ValidateStudentGrades(long StudentGradeId);


    }
}
