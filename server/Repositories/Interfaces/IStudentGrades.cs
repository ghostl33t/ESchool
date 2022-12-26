using server.Models.DTOs.StudentGrades;

namespace server.Repositories.Interfaces
{
    public interface IStudentGrades
    {
        public Task<GetStudentGrades> CreateGradeAsync(PostStudentGrades create);
        public Task<GetStudentGrades> UpdateGradeAsync(long id, PatchStudentGrades update);
        public Task<GetStudentGrades> DeleteGradeAsync(long Id, long deletedbyid);
        public Task<List<GetStudentGrades>> GetGradesForStudent(long StudentId);
        public Task<GetStudentGrades> GetGradesForClass(long ClassId);
        public Task<GetStudentGrades> ValidateStudentGrades(long StudentGradeId);


    }
}
