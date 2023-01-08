using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmailService.DomainModels
{
    public class tempEmail
    {
        public long Id { get; set; }
        public string SenderEmail { get; set; } = string.Empty;
        public string RecipientEmail { get; set; } = string.Empty;
        public long RecipientId { get; set; }
        public string EmailHeader { get; set; } = string.Empty;
        public string EmailText { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Today;

    }
}
