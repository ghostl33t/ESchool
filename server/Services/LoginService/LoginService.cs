using Microsoft.EntityFrameworkCore;
using server.Database;

namespace server.Services.LoginService
{
    public class LoginService : ILoginService
    {
        private readonly DBMain _dbMain;
        public LoginService(DBMain dbMain)
        {
            this._dbMain = dbMain;
        }

        public async Task<Models.Domain.User> Login(Models.DTOs.UsersDTO.Login user)
        {
            var existingUser = await this._dbMain.Users.FirstOrDefaultAsync(s => s.UserName == user.UserName && s.Password == user.Password && s.Deleted == 0);
            if (existingUser != null)
            {
                return existingUser;
            }
            return null;
        }
    }
}
