namespace server.Validations
{
    public interface ISubjectValidations
    {
        public bool validationResult { get; set; }
        public Task<bool> ValidateCreator(long CreatedById);
        public Task<bool> ValidateSerialUnique(string serialNumber);
        public Task<bool> ValidateSerialNumberLength(string serialNumber);
        public Task<bool> ValidateSubjectName(string name);
        //public Task<bool> ValidateSchoolListId(long schoollistId);
        public Task<string> Validation(Models.DTOs.Subject.Create school);
    }
}
