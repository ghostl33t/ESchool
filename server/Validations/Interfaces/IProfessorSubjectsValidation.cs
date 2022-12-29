using server.Models.DTOs.ProfessorSubjects;

namespace server.Validations.Interfaces;

public interface IProfessorSubjectsValidation
{
    public string validationMessage { get; set; }
    public int code { get; set; }

    //Onaj koji dodaje mora biti administrator
    public Task<bool> ValidateCreator(long creatorId);
    //Moguce dodati samo korisnike koji su tipa 1					
    public Task<bool> ValidateProfessorType(long typeOfUser);
    //Nije moguce dodati 2 puta istog profesora za isti predmet
    public Task<bool> ValidateProfessorRepeating(long profsubjid, long professorId,long subjectId);
    //Validacija predmeta
    public Task<bool> ValidateSubject(long subjectId);

    public Task<bool> Validate(PostProfessorSubjects professorSubj);
}
