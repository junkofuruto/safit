using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Safit.Core.Domain.Entities;

[Table("user", Schema = "sf")]
public sealed class User
{
    [Key]
    [Column("id", TypeName = "bigint")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    [Column("username")]
    [MaxLength(20)]
    public string? Username { get; set; }

    [Required]
    [Unicode(true)]
    [Column("password")]
    [MaxLength(64)]
    public string? PasswordHash { get; set; }
}