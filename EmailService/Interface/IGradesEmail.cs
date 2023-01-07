using EmailService.DomainModels;
using server.ServerConnection;

namespace EmailService.Interface
{
    public interface IGradesEmail 
    {
        //SendEmail
        public Task<bool> Execute();
        public Task<bool> SendGradesEmail(tempEmail newEmail);
        public Task<bool> WriteLog(DBMain dbMain, EmailLog emailLog);
        public Task<bool> GetListOfEmails();
    }
}
