using server.Models.DTOs.SchoolList;

namespace server.Validations.Interfaces
{
    public interface ISchoolListValidations
    {
        public string validationMessage { get; set; }
        public int code { get; set; }
        public Task<bool> ValidateSchoolCreator(long CreatedById);
        public Task<bool> ValidateSchoolSerialNumber(string serialNumber);
        public Task<bool> ValidateSchoolName(string schoolName);
        public Task<bool> ValidateSchoolType(int schoolType);
        public Task<bool> Validation(PostSchoolList school);
        public Task<bool> Validation(long Id, PatchUpdate school);
        public Task<bool> Validation(long SchoolListId, long AdministratorId);
    }
}
