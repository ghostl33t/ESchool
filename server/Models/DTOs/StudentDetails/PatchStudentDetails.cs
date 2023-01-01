using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using server.Models.Domain;

namespace server.Models.DTOs.StudentDetails
{
    public class PatchStudentDetails
    {
        public long StudId { get; set; }
        public long ClassDepId { get; set; }
        public int? StudentDiscipline { get; set; }
        public long UpdatedById { get; set; }
        public long ParentId1 { get; set; }
        public long ParentId2 { get; set; }
    }
}