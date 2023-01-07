using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmailService.DomainModels
{
    public class EmailLog
    {
        [Key]
        [Column(TypeName = "bigint")]
        public long Id { get; set; }
        [Column(TypeName = "nvarchar(30)")]
        public string SenderEmail { get; set; } = string.Empty;
        [Column(TypeName = "nvarchar(30)")]
        public string RecipientEmail { get; set; } = string.Empty;
        [Column(TypeName = "bigint")]
        public long RecipientId { get; set; }
        [Column(TypeName = "nvarchar(256)")]
        public string LogMessage { get; set; } = string.Empty;
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; } = DateTime.Today;
    }
}
