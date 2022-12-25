using server.Models.DTOs.ClassDepartment;
using server.Models.DTOs.Subject;
using server.Models.DTOs.UsersDTO;
using server.Validations.Interfaces;

namespace server.Validations.Classes
{
    public class CDSPValidations : ICDSPValidations
    {
        public bool validationResult { get; set; }
        public async Task<bool> ValidateCreator(UsersDTO CreatedBy)
        {
            if (CreatedBy.UserType != 0 || CreatedBy.UserType != 1)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> ValidateSubject(SubjectDTO Subject)
        {
            if (Subject == null)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> ValidateProfessor(UsersDTO User)
        {
            if (User == null || User.UserType != 1)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> ValidateClassDepartment(GetClassDepartment ClassDepDTO)
        {
            if (ClassDepDTO == null)
            {
                return false;
            }
            return true;
        }
        public async Task<string> Validate(SubjectDTO subject, UsersDTO prof, UsersDTO creator, GetClassDepartment classdepdto)
        {
            string message = "";
            validationResult = false;
            if (await ValidateCreator(creator) == false)
            {
                message = "Creator is not valid";
                return message;
            }
            if (await ValidateSubject(subject) == false)
            {
                message = "Subject is not Valid";
                return message;
            }
            if (await ValidateProfessor(prof) == false)
            {
                message = "Professor is not Valid";
                return message;
            }
            if (await ValidateClassDepartment(classdepdto) == false)
            {
                message = "Class department is not valid";
                return message;
            }
            validationResult = true;
            message = "Relation between ClassDep-Prof-Subject created succesfully!";
            return message;

        }
    }
}
