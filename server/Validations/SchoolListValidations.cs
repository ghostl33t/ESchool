﻿using Microsoft.EntityFrameworkCore;
using server.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace server.Validations
{
    public class SchoolListValidations : ISchoolListValidations
    {
        private readonly DBMain DbMain;
        private readonly DBRegistries DBRegistries;
        public bool validationResult { get; set; }
        public SchoolListValidations(DBMain DbMain, DBRegistries dBRegistries)
        {
            this.DbMain = DbMain;
            this.DBRegistries = dBRegistries;
        }
        
        public async Task<bool> ValidateSchoolCreator(long CreatedById)
        {
            var creator = DbMain.Users.FirstOrDefault(s => s.Id == CreatedById && s.Deleted == 0);
            if(creator == null || creator.UserType != 3)
            {
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }
        public async Task<bool> ValidateSerialUnique(string serialNumber)
        {
            var schoolExists = DBRegistries.SchoolList.FirstOrDefault(s => s.SerialNumber == serialNumber && s.Deleted == 0);
            if (schoolExists != null)
            {
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }
        public async Task<bool> ValidateSchoolSerialNumber(string serialNumber)
        {
            if(serialNumber.Length < 3 || serialNumber.Length > 5)
            {
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }
        public async Task<bool> ValidateSchoolName(string schoolName)
        {
            if (schoolName.Length < 5 || schoolName.Length > 15)
            {
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }
        public async Task<bool> ValidateSchoolType(int schoolType)
        {
            if (schoolType < 0 || schoolType > 1)
            {
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }
        public async Task<string> Validation(Models.DTOs.SchoolList.Create school)
        {
            validationResult = false;
            if (await ValidateSchoolCreator(school.CreatedById) == false)
            {
                return await Task.FromResult("You don't have permision to create school!");
            }
            if(await ValidateSerialUnique(school.SerialNumber) == false)
            {
                return await Task.FromResult("School with this serial number already exists in database!");
            } 
            if (await ValidateSchoolSerialNumber(school.SerialNumber) == false)
            {
                return await Task.FromResult("Schools serial number is invalid!");
            }

            if (await ValidateSchoolName(school.Name) == false)
            {
                return await Task.FromResult("School name is invalid");
            }
            if (await ValidateSchoolType(school.SchoolType) == false)
            {
                return await Task.FromResult("School type is invalid!");
            }
            validationResult = true;
            return await Task.FromResult("School added succesfuly!");
        }
    }
}
