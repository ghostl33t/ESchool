﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmailService.DomainModels
{
    public class EmailLog
    {
        public long Id { get; set; }
        public string SenderEmail { get; set; } = string.Empty;
        public string RecipientEmail { get; set; } = string.Empty;
        public long RecipientId { get; set; }
        public string LogMessage { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Today;
    }
}