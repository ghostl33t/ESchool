﻿namespace server.Validations
{
    public interface ISchoolListValidations
    {
        public bool validationResult { get; set; }
        public Task<bool> ValidateSchoolCreator(long CreatedById);
        public Task<bool> ValidateSchoolSerialNumber(string serialNumber);
        public Task<bool> ValidateSchoolName(string schoolName);
        public Task<bool> ValidateSchoolType(int schoolType);
        public Task<string> Validation(Models.DTOs.SchoolList.Create school);
    }
}
