using Microsoft.EntityFrameworkCore;

namespace server.Database
{
    public class DBMain : DbContext
    {
        public DBMain(DbContextOptions<DBMain> options) : base(options)
        {

        }
        //Domain models
        public DbSet<Models.Domain.User> Users { get; set; }
        public DbSet<Models.Domain.ClassDepartment> ClassDepartments { get; set; }
        public DbSet<Models.Domain.ClassDepartmentSubjectProfessor> ClassDepartmentSubjectProfessors { get; set; }
    }
}
