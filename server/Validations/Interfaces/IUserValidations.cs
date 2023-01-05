using server.Models.Domain;

namespace server.Validations.Interfaces;

public interface IUserValidations
{
    public string validationMessage { get; set; }
    public int code { get; set; }
    public Task<bool> ValidateUserNameLength(string username);
    public Task<bool> ValidateUserNameUnique(string username);
    public Task<bool> ValidateUserPassword(string password);
    public Task<bool> ValidateCreateUserByType(UserType creatorType, UserType creationType);
    public Task<bool> ValidateUserNameAndLastName(string nameOrLastName);
    public Task<bool> ValidateUserOIB(string OIB);
    public Task<bool> ValidateUserOIBUnique(string OIB);
    public Task<bool> ValidateUserPhone(string phone);
    public Task<bool> ValidateUserPhoneUnique(string phone);
    public Task<bool> Validate(Models.DTOs.UsersDTO.PostUser user);

    public Task<bool> Validate(long Id, Models.DTOs.UsersDTO.PatchUser user);
    public Task<bool> Validate(long UserId, long AdministratorId);
}
