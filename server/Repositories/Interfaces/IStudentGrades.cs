using server.Models.Domain;

namespace server.Repositories.Interfaces
{
    public interface IStudentGrades
    {
        public  Task<long> CreateGrade(StudentGrades grade);
        public  Task<long> UpdateGrade(long Id, StudentGrades grade);
        public  Task<long> DeleteGrade(long Id, long professorId);
    }
}
