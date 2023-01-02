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
        public DbSet<Models.Domain.StudentDetails> StudentsDetails { get; set; }
        public DbSet<Models.Domain.tempEmail> tempEmails { get; set; }

        public DbSet<Models.Domain.ProfessorSubjects> ProfessorSubjects { get; set; }
        public DbSet<Models.Domain.ClassSubjects> ClassSubjects { get; set; }
        public DbSet<Models.Domain.ClassProfessors> ClassProfessors { get; set; }

        /* VIEWS  FROM REGISTRIES*/
        public DbSet<Models.Domain.Subject> Subjects { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Domain.Subject>()
                .ToView(nameof(Subjects))
                .HasKey(s => s.Id);
        }
    }
}
