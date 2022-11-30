using AutoMapper;
using Microsoft.EntityFrameworkCore;
using server.Database;
using server.Models.DTOs.UsersDTO;
using server.Repositories.Interfaces;
using System.Globalization;

namespace UnitTestProj.FakeRepo
{
    public class UserRepository :IUser
    {
        public  List<UsersDTO> usersDTO { get; set; }
        public UserRepository()
        {
            //Kreiranje dummy podataka za insert 
            usersDTO = new List<server.Models.DTOs.UsersDTO.UsersDTO>()
            {
                new server.Models.DTOs.UsersDTO.UsersDTO()
                {
                    UserName = "azrahalilovic",
                    Password = "azrahalilovic",
                    Name="Azra",
                    LastName="Halilovic",
                    UserType=0,
                    DateOfBirth= new DateTime(2000,05,09,0,0,0),
                    OIB = "123456781235",
                    Phone="38762883757",
                    CreatedDate=DateTime.Today
                },
                new server.Models.DTOs.UsersDTO.UsersDTO()
                {
                    UserName = "amrabratic",
                    Password = "amrabratic",
                    Name="Amra",
                    LastName="Bratic",
                    UserType=0,
                    DateOfBirth= new DateTime(1989,05,09,0,0,0),
                    OIB = "923456781235",
                    Phone="38761883757",
                    CreatedDate=DateTime.Today
                },
                new server.Models.DTOs.UsersDTO.UsersDTO()
                {
                    UserName = "edinpinjic",
                    Password = "edinpinjic",
                    Name="Edin",
                    LastName="Pinjic",
                    UserType=0,
                    DateOfBirth= new DateTime(1993,05,09,0,0,0),
                    OIB = "823456781235",
                    Phone="38761873757",
                    CreatedDate=DateTime.Today
                }
            };
        }

        public async Task<IEnumerable<UsersDTO>> GetAllAsync()
        {
            try
            {
                return usersDTO;
            }
            catch (Exception)
            {

                throw new NotImplementedException();
            }
            
        }

        public Task<UsersDTO> GetUserAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Create> CreateUserAsync(Create newUser)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateUserAsync(Update user)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteUserAsync(Delete user)
        {
            throw new NotImplementedException();
        }

        //public async Task<Models.DTOs.UsersDTO.UsersDTO> GetUserAsync(long id)
        //{
        //    try
        //    {
        //        var user = await DbMain.Users.FirstOrDefaultAsync(s => s.Id == id && s.Deleted == 0);
        //        var userDTO = Mapper.Map<Models.DTOs.UsersDTO.UsersDTO>(user);
        //        return userDTO;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //}
        //public async Task<Models.DTOs.UsersDTO.Create> CreateUserAsync(Models.DTOs.UsersDTO.Create newUser)
        //{
        //    try
        //    {
        //        var user = Mapper.Map<Models.Domain.User>(newUser);
        //        user.CreatedDate = DateTime.Now;
        //        user.Deleted = 0;
        //        user.CreatedBy = await DbMain.Users.FirstOrDefaultAsync(s => s.Id == newUser.CreatedById);
        //        await DbMain.Users.AddAsync(user);
        //        await DbMain.SaveChangesAsync();
        //        return newUser;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //public async Task<string> UpdateUserAsync(Models.DTOs.UsersDTO.Update user)
        //{
        //    var userExist = await DbMain.Users.FirstOrDefaultAsync(s => s.Id == user.Id);
        //    if(userExist != null)
        //    {
        //        userExist.UserName = user.UserName;
        //        userExist.Password = user.Password;
        //        userExist.Name = user.Name;
        //        userExist.LastName = userExist.LastName;
        //        userExist.UserType = user.UserType;
        //        userExist.DateOfBirth = user.DateOfBirth;
        //        //DbMain.Entry(userExist).State = EntityState.Modified;
        //        await DbMain.SaveChangesAsync();
        //    }
        //    return String.Format("User with ID: {0} updated succesfuly!",userExist.Id);
        //}
        //public async Task<string> DeleteUserAsync(Models.DTOs.UsersDTO.Delete user)
        //{
        //    try
        //    {
        //        var userExist = DbMain.Users.FirstOrDefault(s => s.Id == user.Id);
        //        if (userExist != null)
        //        {
        //            userExist.Deleted = user.Deleted;
        //            userExist.DeletedDate = user.DeletedDate;
        //            await DbMain.SaveChangesAsync();
        //        }
        //        return String.Format("User with ID: {0} deleted succesfuly!", user.Id);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    } 
        //}
    }
}
