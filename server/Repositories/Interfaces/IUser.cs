namespace server.Repositories.Interfaces
{
    public interface IUser
    {
        public Task<IEnumerable<Models.DTOs.UsersDTO.UsersDTO>> GetAllAsync();
        public Task<Models.DTOs.UsersDTO.UsersDTO> GetUserAsync(long id);
        public Task<Models.DTOs.UsersDTO.Create> CreateUserAsync(Models.DTOs.UsersDTO.Create newUser);
        public Task<Models.DTOs.UsersDTO.Update> UpdateUserAsync(Models.DTOs.UsersDTO.Update user);
        public Task<bool> DeleteUserAsync(long UserId, long AdministratorId);

    }
}
