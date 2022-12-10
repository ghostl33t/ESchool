using Microsoft.EntityFrameworkCore;

namespace server.Database
{
    public class DBRegistries : DbContext
    {
        public DBRegistries(DbContextOptions<DBRegistries> options) : base(options)
        {

        }
        public DbSet<Models.Domain.SchoolList> SchoolList { get; set; }
        public DbSet<Models.Domain.Subject> Subjects { get; set; }

    }
}
