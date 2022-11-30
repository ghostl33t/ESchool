using Microsoft.EntityFrameworkCore;
using server.Database;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Security.Cryptography;

namespace server.Validations
{
    public class UserValidations : IUserValidations
    {
        private readonly DBMain DbMain;
        public  bool Validated { get; set; }
        public UserValidations(DBMain DbMain)
        {
            this.DbMain = DbMain;
            this.Validated = false;
        }
        public async Task<bool> ValidateUserNameLength(string username)
        {
            if (username == null) { return await Task.FromResult(false); ; }
            if (username.Length < 6 || username.Length > 16) { return await Task.FromResult(false); }
            return await Task.FromResult(true);
        }
        public async Task<bool> ValidateUserNameUnique(string username)
        {
            var userExistWithUserName = await DbMain.Users.FirstOrDefaultAsync(s => s.UserName == username);
            if (userExistWithUserName != null)
            {
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }
        public async Task<bool> ValidateUserPassword(string password)
        {
            if (password == null) { return await Task.FromResult(false); }
            if (password.Length < 8 || password.Length > 16) { return await Task.FromResult(false); }
            return await Task.FromResult(true);

        }
        public async Task<bool> ValidateCreateUserByType(int creatorType, int creationType)
        {
            if (creatorType <= creationType && (creatorType != 3 && creationType != 3) )
            {
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }
        public async Task<bool> ValidateUserNameAndLastName(string nameOrLastName)
        {
            if (nameOrLastName == null || nameOrLastName == "" || nameOrLastName.Length > 25) { return await Task.FromResult(false); }
            return await Task.FromResult(true);
        }
        public async Task<bool> ValidateUserOIB(string OIB)
        {
            if(OIB.Length < 13 || OIB.Length > 13) { return await Task.FromResult(false); }
            return await Task.FromResult(true);
        }
        public async Task<bool> ValidateUserOIBUnique(string OIB)
        {
            var userExists = await DbMain.Users.FirstOrDefaultAsync(s => s.OIB == OIB);
            if(userExists != null)
            {
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }
        public async Task<bool> ValidateUserPhone(string phone)
        {
            if(phone.Length > 12) { return await Task.FromResult(false); }
            return await Task.FromResult(true);
        }
        public async Task<bool> ValidateUserPhoneUnique(string phone)
        {
            
            var userExists = await DbMain.Users.FirstOrDefaultAsync(s => s.Phone == phone);
            if (userExists != null)
            {
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }   
            public async Task<string> CheckUserOnCreation(Models.DTOs.UsersDTO.Create user)
            {
            var creatorType = await DbMain.Users.FirstOrDefaultAsync(s => s.Id == user.CreatedById);
            Validated = false;
            if(user == null)
            {
                return await Task.FromResult("User is not defined!");
            }
            if(await ValidateUserNameLength(user.UserName) == false)
            {
                return await Task.FromResult("Length of username is incorrect!");
            }
            if (await ValidateUserNameUnique(user.UserName) == false)
            {
                return await Task.FromResult(String.Format("User with {0} already exists in database!",user.UserName));
            }
            if (await ValidateUserPassword(user.Password) == false)
            {
                return await Task.FromResult("Password field is incorrect");
            }
            if (await ValidateCreateUserByType(creatorType.UserType,user.UserType) == false)
            {
                return await Task.FromResult("You don't have permission to create new user!");
            }
            if(await ValidateUserNameAndLastName(user.Name) == false)
            {
                return await Task.FromResult("Name is in incorrect format!");
            }
            if (await ValidateUserNameAndLastName(user.LastName) == false)
            {
                return await Task.FromResult("Last name is in incorrect format!");
            }
            if(await ValidateUserOIB(user.OIB) == false)
            {
                return await Task.FromResult("OIB is in incorrect format!");
            }
            if (await ValidateUserOIBUnique(user.OIB) == false)
            {
                return await Task.FromResult("User with typed OIB already exists in database!");
            }
            if (await ValidateUserPhone(user.Phone) == false)
            {
                return await Task.FromResult("Phone is in incorrect format!");
            }
            if (await ValidateUserPhoneUnique(user.Phone) == false)
            {
                return await Task.FromResult("User with typed phone number already exists in database!");
            }
            Validated = true;
            return await Task.FromResult("User added to database succesfuly!");
        }
    }
}
