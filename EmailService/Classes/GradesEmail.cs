using EmailService.Interface;
using MimeKit;
using server.EmailService.Configuration;
using server.DomainModels;
using MailKit.Net.Smtp;
using server.ServerConnection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EmailService.Classes;

public class GradesEmail :   IGradesEmail
{
    private readonly MAILConfiguration _mailConfiguration;
    private readonly IServiceScopeFactory _scopeFactory;
    public GradesEmail(IOptions<MAILConfiguration> mailConfiguration, IServiceScopeFactory scopeFactory)
    {
        _mailConfiguration = mailConfiguration.Value;
        _scopeFactory = scopeFactory;
    }
    public  async Task<bool> Execute()
    {
       await GetListOfEmails();
       return true;
    }
    public async Task<bool> GetListOfEmails()
    {
        using(var scope = _scopeFactory.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<DBMain>();
            var listofEmails = await dbContext.tempEmails.ToListAsync();
            if (listofEmails.Count > 0)
            {
                string[] logMessage = new string[listofEmails.Count];
                foreach (var item in listofEmails.Select((value, i) => new { i, value }))
                {
                    if (await SendGradesEmail(item.value))
                    {
                        logMessage[item.i] = String.Format("EMAIL INFO: Email sent to '{0}' successfuly!", item.value.RecipientEmail);
                        //Zapisat u log sve iz ovog niza stringova

                        //Obrisi emailove iz tempa

                    }
                    else
                    {
                        logMessage[item.i] = String.Format("EMAIL INFO: Email is not sent to '{0}' successfuly!", item.value.RecipientEmail);
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public async Task<bool> SendGradesEmail(tempEmail newEmail)
    {
        try
        {
            var emailObj = new MimeMessage();
            var builder = new BodyBuilder();
            emailObj.Sender = MailboxAddress.Parse(_mailConfiguration.Mail);
            emailObj.To.Add(MailboxAddress.Parse(newEmail.RecipientEmail));
            emailObj.Subject = newEmail.EmailHeader;

            builder.HtmlBody = newEmail.EmailText;
            emailObj.Body = builder.ToMessageBody();
            using var smtpClient = new SmtpClient();
            smtpClient.Connect(_mailConfiguration.Host, _mailConfiguration.Port, MailKit.Security.SecureSocketOptions.StartTls);
            smtpClient.Authenticate(_mailConfiguration.Mail, _mailConfiguration.Password);
            await smtpClient.SendAsync(emailObj);
            smtpClient.Disconnect(true);
            return true;
        }
        catch (Exception)
        {

            throw;
        }
    }
}
