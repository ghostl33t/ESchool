using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace server.Models.Domain
{
    public class StudentDetails
    {
        [Key]
        [Column(TypeName = "bigint")]
        public long Id { get; set; }
        [NotMapped]
        public long StudId { get; set; }
        public User? Student{ get; set; }
        [NotMapped]
        public long ClassDepId { get; set; }
        public ClassDepartment? ClassDepartment { get; set; }
        [Column(TypeName = "smallint")]
        public int StudentDiscipline { get; set; } = 5; //vladanje po defaultu = 5

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

        public long ParId1 { get; set; }
        public long ParId2 { get; set; }
    }
}