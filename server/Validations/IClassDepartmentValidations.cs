namespace server.Validations
{
    public interface IClassDepartmentValidations
    {
        public bool validationResult { get; set; }
        public Task<bool> ValidateCreator(long CreatedById);
        public Task<bool> ValidateSerialUnique(string serialNumber);
        public Task<bool> ValidateClassSerialNumber(string serialNumber);
        public Task<bool> ValidateClassName(string name);
        public Task<bool> ValidateSchoolListId(long schoollistId);
        public Task<string> Validation(Models.DTOs.ClassDepartment.Create school);
    }
}
