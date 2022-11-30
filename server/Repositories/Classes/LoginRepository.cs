using Microsoft.EntityFrameworkCore;
using server.Database;
using server.Repositories.Interfaces;

namespace server.Repositories.Classes
{
    public class LoginRepository : ILogin
    {
        private readonly DBMain DbMain;
        public LoginRepository(DBMain dbMain)
        {
            DbMain = dbMain;
        }

        public async Task<Models.Domain.User> Login(Models.DTOs.UsersDTO.Login user)
        {
            var existingUser = await DbMain.Users.FirstOrDefaultAsync(s => s.UserName == user.UserName && s.Password == user.Password && s.Deleted == 0);
            if(existingUser != null)
            {
                return existingUser;
            }
            return null;
        }
    }
}
