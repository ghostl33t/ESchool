using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models.DTOs.ClassDepartment
{
    public class Update
    {
        public long ID { get; set; }
        public string SerialNumber { get; set; }
        public string Name { get; set; }
        public long SchoolListId { get; set; }

        public int Year { get; set; }
        public long LeaderProfessorId { get; set; }

    }
}
