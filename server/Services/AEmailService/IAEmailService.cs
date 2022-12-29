using server.Models.Domain;

namespace server.Services.AEmailService
{
    public interface IAEmailService
    {
        public Task<bool> PrepareEmail(tempEmail mail);
    }
}
