using AutoMapper;
using Microsoft.EntityFrameworkCore;
using server.Database;
using server.Models.Domain;
using server.Repositories.Interfaces;
using System.Globalization;

namespace server.Repositories.Classes
{
    public class UserRepository : Interfaces.IUser
    {
        private readonly DBMain DbMain;
        private readonly IMapper Mapper;
        public UserRepository(DBMain _DbMain, IMapper _Mapper)
        {
            this.DbMain = _DbMain;
            this.Mapper = _Mapper;
        }
        public async Task<IEnumerable<Models.DTOs.UsersDTO.UsersDTO>> GetAllAsync()
        {
            try
            {
                var allUsers = await DbMain.Users.Where(s => s.Deleted == 0).ToListAsync();
                var allUsersDTO = Mapper.Map<List<Models.DTOs.UsersDTO.UsersDTO>>(allUsers);
                return allUsersDTO;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<Models.DTOs.UsersDTO.UsersDTO> GetUserAsync(long id)
        {
            try
            {
                var user = await DbMain.Users.FirstOrDefaultAsync(s => s.Id == id && s.Deleted == 0);
                var userDTO = Mapper.Map<Models.DTOs.UsersDTO.UsersDTO>(user);
                return userDTO;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public async Task<Models.DTOs.UsersDTO.Create> CreateUserAsync(Models.DTOs.UsersDTO.Create newUser)
        {
            try
            {
                var user = Mapper.Map<Models.Domain.User>(newUser);
                user.CreatedDate = DateTime.Now;
                user.Deleted = 0;
                user.CreatedBy = await DbMain.Users.FirstOrDefaultAsync(s => s.Id == newUser.CreatedById);
                await DbMain.Users.AddAsync(user);
                await DbMain.SaveChangesAsync();
                return newUser;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Models.DTOs.UsersDTO.Update> UpdateUserAsync(Models.DTOs.UsersDTO.Update user)
        {
            var userExist = await DbMain.Users.FirstOrDefaultAsync(s => s.Id == user.Id);
            if(userExist != null)
            {
                userExist.UserName = user.UserName;
                userExist.Password = user.Password;
                userExist.Name = user.Name;
                userExist.LastName = userExist.LastName;
                userExist.UserType = user.UserType;
                userExist.DateOfBirth = user.DateOfBirth;
                //DbMain.Entry(userExist).State = EntityState.Modified;
                await DbMain.SaveChangesAsync();
            }
            return user;
        }
        public async Task<bool> DeleteUserAsync(long UserId, long AdministratorId)
        {
            try
            {
                var userForDelete = await DbMain.Users.FirstOrDefaultAsync(s => s.Id == UserId);
                var Administrator = await DbMain.Users.FirstOrDefaultAsync(s => s.Id == AdministratorId);
                if (userForDelete != null && Administrator != null)
                {
                    userForDelete.Deleted = 1;
                    userForDelete.DeletedDate = DateTime.Today;
                    userForDelete.DeletedById = AdministratorId;
                    await DbMain.SaveChangesAsync();
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
