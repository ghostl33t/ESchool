using server.Database;

namespace server.Services.LoginService
{
    public interface ILoginService
    {
        public Task<Models.Domain.User> Login(Models.DTOs.UsersDTO.Login user);

    }
}
