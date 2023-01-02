using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using server.Models.Domain;

namespace server.Models.DTOs.StudentDetails
{
    public class PostStudentDetails
    {
        public long StudentId_ { get; set; }
        public long ClassDepartmentId_ { get; set; }
        public int? StudentDiscipline { get; set; } = 5; 
        public long CreatedById { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public long ParentId1 { get; set; }
        public long ParentId2 { get; set; }
        public int Deleted { get; set; } = 0;
    }
}