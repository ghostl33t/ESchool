using Microsoft.EntityFrameworkCore;
using server.Database;
using server.Models.Domain;

namespace server.Repositories.Classes
{
    public class UserRepository : Interfaces.IUser
    {
        private readonly DBMain _dbMain;
        public UserRepository(DBMain dbMain)
        {
            this._dbMain = dbMain;
        }
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            try
            {
                var allUsers = await _dbMain.Users.Where(s => s.Deleted == 0).ToListAsync();

                return allUsers;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<User> GetUserAsync(long id)
        {
            try
            {
                var user = await _dbMain.Users.FirstOrDefaultAsync(s => s.Id == id && s.Deleted == 0);
                return user;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public async Task<long> CreateUserAsync(User newUser)
        {
            try
            {
                newUser.CreatedDate = DateTime.Now;
                newUser.Deleted = 0;
                await _dbMain.Users.AddAsync(newUser);
                await _dbMain.SaveChangesAsync();
                return newUser.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<long> UpdateUserAsync(User user)
        {
            var userExist = await _dbMain.Users.FirstOrDefaultAsync(s => s.Id == user.Id);
            if(userExist != null)
            {
                _dbMain.Users.Update(user);
                //DbMain.Entry(userExist).State = EntityState.Modified;
                await _dbMain.SaveChangesAsync();
            }
            return userExist.Id;
        }
        public async Task<bool> DeleteUserAsync(long UserId, long AdministratorId)
        {
            try
            {
                var userForDelete = await _dbMain.Users.FirstOrDefaultAsync(s => s.Id == UserId);
                var Administrator = await _dbMain.Users.FirstOrDefaultAsync(s => s.Id == AdministratorId);
                if (userForDelete != null && Administrator != null)
                {
                    userForDelete.Deleted = 1;
                    userForDelete.DeletedDate = DateTime.Today;
                    userForDelete.DeletedById = AdministratorId;
                    await _dbMain.SaveChangesAsync();
                    return true;
                }
                else return false;
            }
            catch (Exception)
            {
                throw;
            } 
        }
    }
}
