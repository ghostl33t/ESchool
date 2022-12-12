using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace server.Models.Domain
{
    public class StudentGrades
    {
        [Key]
        [Column(TypeName = "bigint")]
        public long Id { get; set; }
        public User? Student { get; set; }
        [Column(TypeName ="smallint")]
        public int Grade { get; set; }
        public ClassDepartmentSubjectProfessor? ClassDepartmentSubjectProfessor { get; set; }
        [Column(TypeName = "nvarchar")]
        [MaxLength(150)]
        public string Description { get; set; } = "";
        [Column(TypeName = "smallint")]
        public int Validated { get; set; } = 0;
        [Column(TypeName = "bigint")]
        public long CreatedById { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Column(TypeName = "smallint")]
        public int Deleted { get; set; } = 0;

        [Column(TypeName = "datetime")]
        public DateTime? DeletedDate { get; set; }
        [Column(TypeName = "bigint")]
        public long DeletedById { get; set; }
    }
}