using server.Models.DTOs.ClassSubjects;

namespace server.Validations.Interfaces
{
    public interface IClassSubjectsValidations
    {
        public string validationMessage { get; set; } 
        public int code { get; set; }

        public Task<bool> ValidateCreator(long leaderId, long classDepId); //Validirati da li osoba koja dodaje predmet je zapravo ClassLeader iz tabele Classdeparmtent
        public Task<bool> ValidateSubject(long Id); //Validirati da li postoji predmet
        public Task<bool> ValidateClassDepartment(long Id); //Validirati da li postoji ClassDepartment

        public Task<bool> Validate(PostClassSubjects classSubject);
        public Task<bool> Validate(long Id, PatchClassSubjects classSubject);
        public Task<bool> Validate(long Id, long leaderId);

    }
}
