using server.Models.Domain;

namespace server.Repositories.Interfaces;

public interface IClassProfessor
{
    public  Task<long> CreateClassProfessor(ClassProfessors classProfessor);
    public  Task<long> UpdateClassProfessor(long Id, ClassProfessors classProfessor);
    public  Task<long> DeleteClassProfessor(long classProfId, long leaderId);
}
