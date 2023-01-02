using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace server.Models.Domain
{
    public class ClassProfessors
    {
        [Key]
        [Column(TypeName = "bigint")]
        public long ID { get; set; }

        public User? Professor { get; set; }
        public ClassDepartment? ClassDepartment { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Column(TypeName = "datetime")]
        public User? CreatedBy { get; set; }
        public long DeletedById { get; set; } = 0;
        public DateTime? DeletedDate { get; set; } = null;
        [Column(TypeName = "smallint")]
        public int? Deleted { get; set; }

        //Notmapped objects

        [NotMapped]
        public long ProfessorId_ { get; set; }
        [NotMapped]
        public long ClassDepartmentId_ { get; set; }
        [NotMapped]
        public long CreatedById_ { get; set; }
    }
}
