using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using server.Models.Domain;

namespace server.Models.DTOs.StudentDetails
{
    public class GetStudentDetails
    {
        public string Name { get; set; }
        public string SchoolType { get; set; }
        public string ClassGrade { get; set; }
    }
}