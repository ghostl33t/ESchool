using server.Models.Domain;
using server.Models.DTOs.StudentGrades;

namespace server.Repositories.Interfaces
{
    public interface IStudentGrades
    {
        public Task<List<GetStudentGrades>> GetGradesForStudent(long Id);
        public  Task<long> CreateGrade(StudentGrades grade);
        public  Task<long> UpdateGrade(long Id, StudentGrades grade);
        public  Task<long> DeleteGrade(long Id, long professorId);

        public Task<float> AverageGrade(long classDepartmentId, long studentId);
        public Task<float[,]> BestWorstSubjectGrade(long classDepartmentId, long studentId);
    }
}
