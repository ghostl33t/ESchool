using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models.Domain
{
    public class ClassDepartment
    {
        [Key]
        [Column(TypeName = "bigint")]
        public long ID { get; set; }
        [Column(TypeName = "nvarchar")]
        [MinLength(3)]
        [MaxLength(5)]
        public string SerialNumber { get; set; }
        [Column(TypeName = "nvarchar")]
        [MaxLength(15)]
        public string Name { get; set; }
        [Column(TypeName="bigint")]
        public long SchoolListId { get; set; }

        public int Year { get; set; }
        public User? LeaderProfessor { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Column(TypeName = "datetime")]
        public User? CreatedBy { get; set; }
        public long DeletedById { get; set; }
        public DateTime? DeletedDate { get; set; } = DateTime.Now;
        [Column(TypeName = "smallint")]
        public int? Deleted { get; set; }

    }
}
