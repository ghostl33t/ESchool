using server.DomainModels;

namespace EmailService.Interface
{
    public interface IGradesEmail 
    {
        //SendEmail
        public Task<bool> Execute();
        public Task<bool> SendGradesEmail(tempEmail newEmail);
        public Task<bool> GetListOfEmails();
    }
}
