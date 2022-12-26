namespace server.Validations.Interfaces
{
    public interface ISubjectValidations
    {
        public string validationMessage { get; set; }
        public int code { get; set; }
        public Task<bool> ValidateCreator(long CreatedById);
        public Task<bool> ValidateSerialUnique(string serialNumber);
        public Task<bool> ValidateSerialNumberLength(string serialNumber);
        public Task<bool> ValidateSubjectName(string name);
        public Task<bool> Validation(Models.DTOs.Subject.Create school);
        public Task<bool> Validation(Models.DTOs.Subject.Update school);
        public Task<bool> Validation(long SubjectId, long AdministratorId);
    }
}
