using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Index("Email", Name = "UQ__Users__A9D1053420AB7864", IsUnique = true)]
public partial class User
{
    [Key]
    public int UserId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Firstname { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Lastname { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [Unicode(false)]
    public string? PasswordHash { get; set; }

    [Unicode(false)]
    public string? PasswordSalt { get; set; }

    public int? RoleId { get; set; }

    public bool IsRecordActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    public int? CreatedByUserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastModificationAt { get; set; }

    public int? LastModificationByUserId { get; set; }

    [ForeignKey("RoleId")]
    [InverseProperty("Users")]
    public virtual UserRole? Role { get; set; }
}
