namespace server.Repositories.Interfaces
{
    public interface IUser
    {
        public Task<IEnumerable<Models.DTOs.UsersDTO.UsersDTO>> GetAllAsync();
        public Task<Models.DTOs.UsersDTO.UsersDTO> GetUserAsync(long id);
        public Task<Models.DTOs.UsersDTO.Create> CreateUserAsync(Models.DTOs.UsersDTO.Create newUser);
        public Task<string> UpdateUserAsync(Models.DTOs.UsersDTO.Update user);
        public Task<string> DeleteUserAsync(Models.DTOs.UsersDTO.Delete user);

    }
}
