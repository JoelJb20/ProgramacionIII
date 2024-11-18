using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Application.DTO;

public class UserDTO
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

    public int? CreatedByUserId { get; set; }
}
