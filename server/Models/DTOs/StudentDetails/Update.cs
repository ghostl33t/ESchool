using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using server.Models.Domain;

namespace server.Models.DTOs.StudentDetails
{
    public class Update
    {
        public long Id { get; set; }
        public User? Student { get; set; }
        public server.Models.Domain.ClassDepartment? ClassDepartment { get; set; }
        public int? StudentDiscipline { get; set; }
    }
}