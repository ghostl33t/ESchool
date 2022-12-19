using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models.DTOs.SchoolList
{
    public class SchoolList
    {
        public long Id { get; set; }
        public string SerialNumber { get; set; } = "";
        public string Name { get; set; } = "";
        public int SchoolType { get; set; }

        public long CreatedById { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int Deleted { get; set; } = 0;
        public DateTime? DeletedDate { get; set; }
        public long DeletedById { get; set; }



    }
}
