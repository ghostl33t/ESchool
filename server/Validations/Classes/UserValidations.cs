using Microsoft.EntityFrameworkCore;
using server.Database;
using server.Validations.Interfaces;
namespace server.Validations.Classes;

public class UserValidations : IUserValidations
{
    private readonly DBMain _dbMain;
    public string validationMessage { get; set; } = String.Empty;
    public int code { get; set; }
    public UserValidations(DBMain dbMain)
    {
        this._dbMain = dbMain;
    }
    public async Task<bool> ValidateUserNameLength(string username)
    {
        if (username == null) { return await Task.FromResult(false); ; }
        if (username.Length < 6 || username.Length > 16) { return await Task.FromResult(false); }
        return await Task.FromResult(true);
    }
    public async Task<bool> ValidateUserNameUnique(string username)
    {
        var userExistWithUserName = await _dbMain.Users.FirstOrDefaultAsync(s => s.UserName == username);
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
        var userExists = await _dbMain.Users.FirstOrDefaultAsync(s => s.OIB == OIB);
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

        var userExists = await _dbMain.Users.FirstOrDefaultAsync(s => s.Phone == phone);
        if (userExists != null)
        {
            return await Task.FromResult(false);
        }
        return await Task.FromResult(true);
    }
    public async Task<bool> Validate(Models.DTOs.UsersDTO.PostUser user)
    {
        code = 0;
        if (user != null)
        {
            var creatorType = await _dbMain.Users.FirstOrDefaultAsync(s => s.Id == user.CreatedById);
            if (creatorType == null)
            {
                code = 401;
                validationMessage = "You don't have permission to create new user!";
            }
            if (await ValidateCreateUserByType(creatorType.UserType, user.UserType) == false)
            {
                code = 401;
                validationMessage = "You don't have permission to create new user!";
            }
            if (await ValidateUserNameLength(user.UserName) == false)
            {
                code = 400;
                validationMessage = "Length of username is incorrect!";
            }
            if (await ValidateUserNameUnique(user.UserName) == false)
            {
                code = 400;
                validationMessage = string.Format("User with {0} already exists in database!", user.UserName);
            }
            if (await ValidateUserPassword(user.Password) == false)
            {
                code = 400;
                validationMessage = "Password field is incorrect";
            }
            if (await ValidateUserNameAndLastName(user.Name) == false)
            {
                code = 400;
                validationMessage = "Name is in incorrect format!";
            }
            if (await ValidateUserNameAndLastName(user.LastName) == false)
            {
                code = 400;
                validationMessage = "Last name is in incorrect format!";
            }
            if (await ValidateUserOIB(user.OIB) == false)
            {
                code = 400;
                validationMessage = "OIB is in incorrect format!";
            }
            if (await ValidateUserOIBUnique(user.OIB) == false)
            {
                code = 400;
                validationMessage = "User with typed OIB already exists in database!";
            }
            if (await ValidateUserPhone(user.Phone) == false)
            {
                code = 400;
                validationMessage = "Phone is in incorrect format!";
            }
            if (await ValidateUserPhoneUnique(user.Phone) == false)
            {
                code = 400;
                validationMessage = "User with typed phone number already exists in database!";
            }
        }
        else
        {
            code = 400;
            validationMessage = "User is not defined!";
        }
        if(code != 0) { return false; }
        code = 201;
        validationMessage = "User added to database successfuly!";
        return true;
    }

    public async Task<bool> Validate(Models.DTOs.UsersDTO.PatchUser user)
    {
        var creatorType = await _dbMain.Users.FirstOrDefaultAsync(s => s.Id == user.UpdatedById);
        var userExist = await _dbMain.Users.FirstOrDefaultAsync(s => s.Id == user.Id);
        code = 0;
        if(creatorType == null)
        {
            code = 401;
            validationMessage = "You don't have permission to create new user!";
        }
        if (await ValidateCreateUserByType(creatorType.UserType, user.UserType) == false)
        {
            code = 401;
            validationMessage = "You don't have permission to create new user!";
        }
        if (user == null || userExist == null)
        {
            code = 400;
            validationMessage = "User is not defined!";
        }
        if(userExist.UserName != user.UserName) { 
            if (await ValidateUserNameLength(user.UserName) == false)
            {
                code = 400;
                validationMessage = "Length of username is incorrect!";
            }
            if (await ValidateUserNameUnique(user.UserName) == false)
            {
                code = 400;
                validationMessage = string.Format("User with {0} already exists in database!", user.UserName);
            }
        }
        
        if (await ValidateUserPassword(user.Password) == false)
        {
            code = 400;
            validationMessage = "Password field is incorrect";
        }
        if(userExist.Name != user.Name)
        {
            if (await ValidateUserNameAndLastName(user.Name) == false)
            {
                code = 400;
                validationMessage = "Name is in incorrect format!";
            }
        }
        if (userExist.LastName != user.LastName)
        {
            if (await ValidateUserNameAndLastName(user.LastName) == false)
            {
                code = 400;
                validationMessage = "Last name is in incorrect format!";
            }
        }
        if (userExist.OIB != user.OIB)
        {
            if (await ValidateUserOIB(user.OIB) == false)
            {
                code = 400;
                validationMessage = "OIB is in incorrect format!";
            }
            if (await ValidateUserOIBUnique(user.OIB) == false)
            {
                code = 400;
                validationMessage = "User with typed OIB already exists in database!";
            }
        }
        if(userExist.Phone != user.Phone)
        {
            if (await ValidateUserPhone(user.Phone) == false)
            {
                code = 400;
                validationMessage = "Phone is in incorrect format!";
            }
            if (await ValidateUserPhoneUnique(user.Phone) == false)
            {
                code = 400;
                validationMessage = "User with typed phone number already exists in database!";
            }
        }
        if(code != 0) { return false; }
        code = 204;
        validationMessage = "User updated successfuly!";
        return true;
    }
    public  async Task<bool> Validate(long UserId, long AdministratorId)
    {
        var admin = await _dbMain.Users.FirstOrDefaultAsync(s => s.Id == AdministratorId);
        var userExist = await _dbMain.Users.FirstOrDefaultAsync(s => s.Id == UserId);
        code = 401;
        if(admin != null && userExist != null)
        {
            if (admin.UserType == 0)
            {
                code = 204;
            }
        }
        if(code != 204)
        {
            validationMessage = "Failed";
            return false;
            
        }
        validationMessage = "User deleted successfuly";
        return true;
    }
}
