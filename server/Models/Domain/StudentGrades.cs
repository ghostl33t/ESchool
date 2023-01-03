using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models.Domain
{
    public class StudentGrades
    {
        [Key]
        [Column(TypeName ="bigint")]
        public long Id { get; set; }
        [Column(TypeName = "bigint")]
        public long StudentId { get; set; }
        [Column(TypeName = "bigint")]
        public long ProfessorId { get; set; }
        [Column(TypeName = "bigint")]
        public long SubjectId { get; set; }
        [Column(TypeName = "smallint")]
        public int Grade { get; set; }
        [Column(TypeName = "nvarchar")]
        public string Description { get; set; } = String.Empty;
        [Column(TypeName = "datetime")]
        public DateTime GradeDate { get; set; } = DateTime.Now;
    }
}
