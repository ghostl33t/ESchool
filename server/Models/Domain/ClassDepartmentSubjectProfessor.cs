using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace server.Models.Domain
{
    public class ClassDepartmentSubjectProfessor
    {
        [Key]
        [Column(TypeName = "bigint")]
        public long Id { get; set; }
        [NotMapped]
        public long ClassDepId { get; set; }
        public ClassDepartment? ClassDepartment { get; set; }
        [Column(TypeName = "bigint")]
        public long SubjectID { get; set; }

        [NotMapped]
        public long ProfessorId { get; set; }
        public User? UserProfessor  { get; set; }

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
