using server.Database;

namespace server.Repositories.Interfaces
{
    public interface ILogin 
    {
        public Task<Models.Domain.User> Login(Models.DTOs.UsersDTO.Login user);
        
    }
}
