namespace server.Repositories.Interfaces
{
    public interface ITokenHandler
    {
        public Task<string> CreateTokenAsync(Models.Domain.User user);
    }
}
