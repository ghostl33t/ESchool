using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace server.Models.Domain
{
    public class ProfessorSubjects
    {
        [Key]
        [Column(TypeName = "bigint")]
        public long ID { get; set; }

        public User? Professor { get; set; }

        public long SubjectId { get; set; }

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
        public long CreatedById_ { get; set; } 
    }
}
