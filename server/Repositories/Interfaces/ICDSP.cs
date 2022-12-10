namespace server.Repositories.Interfaces
{
    public interface ICDSP
    {
        public Task<List<server.Models.DTOs.ClassDepartmentSubjectProfessor.ClassDetails>> GetClassDetails(long classdepid);
        public Task<List<server.Models.DTOs.ClassDepartmentSubjectProfessor.ProfesorSubjectDetails>> GetProfessorSubjectDetails(long professorId);

    }
}
