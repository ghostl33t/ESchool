using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models.DTOs.Subject
{
    public class PatchSubject
    {
        public long Id { get; set; }
        public string SerialNumber { get; set; } = "";
        public string Name { get; set; } = "";
        public long UpdatedById { get; set; }

    }
}
