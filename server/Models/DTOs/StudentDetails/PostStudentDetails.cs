using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using server.Models.Domain;

namespace server.Models.DTOs.StudentDetails
{
    public class PostStudentDetails
    {
        public long StudId { get; set; }
        public long ClassDepId { get; set; }
        public int? StudentDiscipline { get; set; } = 5; 
        public long CreatedById { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}