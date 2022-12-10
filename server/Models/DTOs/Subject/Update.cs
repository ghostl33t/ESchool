using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models.DTOs.Subject
{
    public class Update
    {
        public long ID { get; set; }
        public string SerialNumber { get; set; }
        public string Name { get; set; }
        public int SchoolType { get; set; }

    }
}
