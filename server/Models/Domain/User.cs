using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models.Domain;

public enum UserType
{
    Administrator = 0,
    Professor = 1,
    Student = 2,
    Parent = 3
}
public class User
{
    [Key]
    [Column(TypeName="bigint")]
    public long Id { get; set; }

    [Column(TypeName ="nvarchar")]
    [MinLength(6)]
    [MaxLength(16)]
    public string? UserName { get; set; }

    [Column(TypeName = "nvarchar")]
    [MinLength(8)]
    [MaxLength(16)]
    public string? Password { get; set; }
    [Column(TypeName = "nvarchar")]
    [MaxLength(25)]
    
    public string? Name { get; set; }
    [Column(TypeName = "nvarchar")]
    [MaxLength(25)]
    public string? LastName { get; set; }

    [Column(TypeName = "nvarchar")]
    [MaxLength(30)]
    public string? Email { get; set; } = "";

    [Column(TypeName = "nvarchar")]
    [MaxLength(12)]
    public string? Phone { get; set; }

    [Column(TypeName ="smallint")]
    public UserType UserType { get; set; }
    [Column(TypeName ="date")]
    public DateTime DateOfBirth { get; set; }
    [Column(TypeName ="nvarchar")]
    [MinLength(13)]
    [MaxLength(13)]
    public string? OIB { get; set; }
    [Column(TypeName ="datetime")]
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    [Column(TypeName = "datetime")]
    public User? CreatedBy { get; set; }

    public long DeletedById { get; set; }
    public DateTime? DeletedDate { get; set; } 
    [Column(TypeName ="smallint")]
    public int? Deleted { get; set; }
}

