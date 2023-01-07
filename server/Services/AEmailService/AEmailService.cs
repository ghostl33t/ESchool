using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using server.Database;
using server.Models.Domain;

namespace server.Services.AEmailService
{
    public class AEmailService : IAEmailService
    {
        private readonly DBMain _dbMain;
        public AEmailService(DBMain dbMain)
        {
            _dbMain = dbMain;
        }
        public async Task<bool> PrepareEmail(tempEmail mail)
        {
            try
            {
                await _dbMain.tempEmails.AddAsync(mail);
                await _dbMain.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
