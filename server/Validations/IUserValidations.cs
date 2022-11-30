using server.Database;

namespace server.Validations
{
    public interface IUserValidations
    {
        public bool Validated { get; set; }
        public Task<bool> ValidateUserNameLength(string username);
        public Task<bool> ValidateUserNameUnique(string username);
        public Task<bool> ValidateUserPassword(string password);
        public Task<bool> ValidateCreateUserByType(int creatorType, int creationType);
        public Task<bool> ValidateUserNameAndLastName(string nameOrLastName);
        public Task<bool> ValidateUserOIB(string OIB);
        public Task<bool> ValidateUserOIBUnique(string OIB);
        public Task<bool> ValidateUserPhone(string phone);
        public Task<bool> ValidateUserPhoneUnique(string phone);
        public Task<string> CheckUserOnCreation(Models.DTOs.UsersDTO.Create user);


    }
}
