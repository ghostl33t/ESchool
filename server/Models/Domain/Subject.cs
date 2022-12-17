﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace server.Models.Domain
{
    public class Subject
    {
        [Key]
        [Column(TypeName = "bigint")]
        public long Id { get; set; } 
        [Column(TypeName = "nvarchar")]
        [MinLength(3)]
        [MaxLength(5)]
        public string SerialNumber { get; set; } = "";
        [Column(TypeName = "nvarchar")]
        [MaxLength(15)]
        public string Name { get; set; } = "";
        [Column(TypeName = "bigint")]
        public long CreatedById { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Column(TypeName = "smallint")]
        public int Deleted { get; set; } = 0;

        [Column(TypeName = "datetime")]
        public DateTime? DeletedDate { get; set; }
        [Column(TypeName = "bigint")]
        public long DeletedById { get; set; }
    }
}
