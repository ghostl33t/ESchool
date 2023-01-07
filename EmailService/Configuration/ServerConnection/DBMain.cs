using EmailService.DomainModels;
using Microsoft.EntityFrameworkCore;
namespace server.ServerConnection
{
    public class DBMain : DbContext
    {
        public DBMain(DbContextOptions<DBMain> options) : base(options)
        {

        }
        public DbSet<tempEmail> tempEmails { get; set; }
        public DbSet<EmailLog> EmailLog { get; set; }
    }
}
