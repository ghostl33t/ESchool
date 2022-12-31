using server.Models.Domain;
using server.Models.DTOs.ProfessorSubjects;

namespace server.Repositories.Interfaces;

public interface IProfessorSubjectsRepository
{
    //get
    public Task<List<GetProfessorSubjects>> GetProfessorsAndSubjects();
    //post
    public Task<long> CreateProfSubj(ProfessorSubjects newProfSubj);
    public Task<long> UpdateProfSubj(ProfessorSubjects profsubjdto);
    public Task<long> DeleteProfSubj(long Id, long AdministratorId);
}
