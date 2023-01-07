using Microsoft.EntityFrameworkCore;
using server.DomainModels;
using System.Data;

namespace server.ServerConnection
{
    public class DBMain : DbContext
    {
        public DBMain(DbContextOptions<DBMain> options) : base(options)
        {

        }
        public DbSet<tempEmail> tempEmails { get; set; }
    }
}
