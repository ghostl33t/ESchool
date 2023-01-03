using Microsoft.EntityFrameworkCore;
using server.Models.Domain;
using System.Data;

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
        public DbSet<Models.Domain.StudentGrades> StudentGrades { get; set; }

        /* VIEWS  FROM REGISTRIES*/
        public DbSet<Models.Domain.Subject> Subjects { get; set; }
        public DbSet<Models.Domain.SchoolList> SchoolList { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Domain.Subject>()
                .ToView(nameof(Subjects))
                .ToTable(nameof(DBRegistries.Subjects))
                .HasKey(s => s.Id);
            modelBuilder.Entity<Models.Domain.SchoolList>()
                .ToView(nameof(SchoolList))
                .ToTable(nameof(DBRegistries.SchoolList))
                .HasKey(s => s.Id);
        }
    }
}
