using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models.Domain
{
    public class tempEmail
    {
        [Key]
        [Column(TypeName ="bigint")]
        public long Id { get; set; }
        [Column(TypeName = "nvarchar(30)")]
        public string SenderEmail { get; set; } = string.Empty;
        [Column(TypeName = "nvarchar(30)")]
        public string RecipientEmail { get; set; } = string.Empty;
        [Column(TypeName = "bigint")]
        public long RecipientId { get; set; }
        [Column(TypeName ="nvarchar(20)")]
        public string EmailHeader { get; set; } = string.Empty;
        [Column(TypeName ="nvarchar(4000)")]
        public string EmailText { get; set; } = string.Empty;
        [Column(TypeName ="datetime")]
        public DateTime CreatedDate { get; set; } = DateTime.Today;

    }
}
