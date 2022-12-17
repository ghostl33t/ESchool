namespace server.Validations.Interfaces
{
    public interface ISubjectValidations
    {
        public bool validationResult { get; set; }
        public int code { get; set; }
        public Task<bool> ValidateCreator(long CreatedById);
        public Task<bool> ValidateSerialUnique(string serialNumber);
        public Task<bool> ValidateSerialNumberLength(string serialNumber);
        public Task<bool> ValidateSubjectName(string name);
        public Task<string> Validation(Models.DTOs.Subject.Create school);
        public Task<string> Validation(Models.DTOs.Subject.Update school);
        public Task<string> Validation(long SubjectId, long AdministratorId);
    }
}
