using Microsoft.EntityFrameworkCore;
using server.Database;
using server.Validations.Interfaces;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Security.Cryptography;

namespace server.Validations.Classes
{
    public class UserValidations : IUserValidations
    {
        private readonly DBMain DbMain;
        public int code { get; set; } = 404;
        public bool Validated { get; set; }
        public UserValidations(DBMain DbMain)
        {
            this.DbMain = DbMain;
            Validated = false;
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
            if (creatorType <= creationType && creatorType != 0 && creationType != 0)
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
            if (OIB.Length < 13 || OIB.Length > 13) { return await Task.FromResult(false); }
            return await Task.FromResult(true);
        }
        public async Task<bool> ValidateUserOIBUnique(string OIB)
        {
            var userExists = await DbMain.Users.FirstOrDefaultAsync(s => s.OIB == OIB);
            if (userExists != null)
            {
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }
        public async Task<bool> ValidateUserPhone(string phone)
        {
            if (phone.Length > 12) { return await Task.FromResult(false); }
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
        public async Task<string> Validate(Models.DTOs.UsersDTO.Create user)
        {
            var creatorType = await DbMain.Users.FirstOrDefaultAsync(s => s.Id == user.CreatedById);
            Validated = false;
            if (creatorType == null)
            {
                code = 401;
                return await Task.FromResult("You don't have permission to create new user!");
            }
            if (await ValidateCreateUserByType(creatorType.UserType, user.UserType) == false)
            {
                code = 401;
                return await Task.FromResult("You don't have permission to create new user!");
            }
            if (user == null)
            {
                code = 400;
                return await Task.FromResult("User is not defined!");
            }
            if (await ValidateUserNameLength(user.UserName) == false)
            {
                code = 400;
                return await Task.FromResult("Length of username is incorrect!");
            }
            if (await ValidateUserNameUnique(user.UserName) == false)
            {
                code = 400;
                return await Task.FromResult(string.Format("User with {0} already exists in database!", user.UserName));
            }
            if (await ValidateUserPassword(user.Password) == false)
            {
                code = 400;
                return await Task.FromResult("Password field is incorrect");
            }
            if (await ValidateUserNameAndLastName(user.Name) == false)
            {
                code = 400;
                return await Task.FromResult("Name is in incorrect format!");
            }
            if (await ValidateUserNameAndLastName(user.LastName) == false)
            {
                code = 400;
                return await Task.FromResult("Last name is in incorrect format!");
            }
            if (await ValidateUserOIB(user.OIB) == false)
            {
                code = 400;
                return await Task.FromResult("OIB is in incorrect format!");
            }
            if (await ValidateUserOIBUnique(user.OIB) == false)
            {
                code = 400;
                return await Task.FromResult("User with typed OIB already exists in database!");
            }
            if (await ValidateUserPhone(user.Phone) == false)
            {
                code = 400;
                return await Task.FromResult("Phone is in incorrect format!");
            }
            if (await ValidateUserPhoneUnique(user.Phone) == false)
            {
                code = 400;
                return await Task.FromResult("User with typed phone number already exists in database!");
            }
            Validated = true;
            code = 201;
            return await Task.FromResult("User added to database succesfuly!");
        }

        public async Task<string> Validate(Models.DTOs.UsersDTO.Update user)
        {
            var creatorType = await DbMain.Users.FirstOrDefaultAsync(s => s.Id == user.UpdatedById);
            var userExist = await DbMain.Users.FirstOrDefaultAsync(s => s.Id == user.Id);
            Validated = false;
            if(creatorType == null)
            {
                code = 401;
                return await Task.FromResult("You don't have permission to create new user!");
            }
            if (await ValidateCreateUserByType(creatorType.UserType, user.UserType) == false)
            {
                code = 401;
                return await Task.FromResult("You don't have permission to create new user!");
            }
            if (user == null || userExist == null)
            {
                code = 400;
                return await Task.FromResult("User is not defined!");
            }
            if(userExist.UserName != user.UserName) { 
                if (await ValidateUserNameLength(user.UserName) == false)
                {
                    code = 400;
                    return await Task.FromResult("Length of username is incorrect!");
                }
                if (await ValidateUserNameUnique(user.UserName) == false)
                {
                    code = 400;
                    return await Task.FromResult(string.Format("User with {0} already exists in database!", user.UserName));
                }
            }
            
            if (await ValidateUserPassword(user.Password) == false)
            {
                code = 400;
                return await Task.FromResult("Password field is incorrect");
            }
            if(userExist.Name != user.Name)
            {
                if (await ValidateUserNameAndLastName(user.Name) == false)
                {
                    code = 400;
                    return await Task.FromResult("Name is in incorrect format!");
                }
            }
            if (userExist.LastName != user.LastName)
            {
                if (await ValidateUserNameAndLastName(user.LastName) == false)
                {
                    code = 400;
                    return await Task.FromResult("Last name is in incorrect format!");
                }
            }
            if (userExist.OIB != user.OIB)
            {
                if (await ValidateUserOIB(user.OIB) == false)
                {
                    code = 400;
                    return await Task.FromResult("OIB is in incorrect format!");
                }
                if (await ValidateUserOIBUnique(user.OIB) == false)
                {
                    code = 400;
                    return await Task.FromResult("User with typed OIB already exists in database!");
                }
            }
            if(userExist.Phone != user.Phone)
            {
                if (await ValidateUserPhone(user.Phone) == false)
                {
                    code = 400;
                    return await Task.FromResult("Phone is in incorrect format!");
                }
                if (await ValidateUserPhoneUnique(user.Phone) == false)
                {
                    code = 400;
                    return await Task.FromResult("User with typed phone number already exists in database!");
                }
            }
            code = 204;
            Validated = true;
            return await Task.FromResult("User updated succesfuly!");
        }
        public  async Task<string> Validate(long UserId, long AdministratorId)
        {
            var admin = await DbMain.Users.FirstOrDefaultAsync(s => s.Id == AdministratorId);
            var userExist = await DbMain.Users.FirstOrDefaultAsync(s => s.Id == UserId);
            Validated = false;
            if(admin != null && userExist != null)
            {
                if (admin.UserType == 0)
                {
                    Validated = true;
                    code = 204;
                }
            }
            if(Validated == true)
            {
                return await Task.FromResult("User deleted succesfuly");
            }
            else
            {
                return await Task.FromResult("Failed");
            }
            
            
        }
    }
}
