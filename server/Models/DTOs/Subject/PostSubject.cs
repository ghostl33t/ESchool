using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models.DTOs.Subject
{
    public class PostSubject
    {
        public string SerialNumber { get; set; } = "";
        public string Name { get; set; } = "";
        public long CreatedById { get; set; }

    }
}
