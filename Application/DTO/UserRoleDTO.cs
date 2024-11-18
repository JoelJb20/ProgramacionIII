using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Application.DTO;

public class UserRoleDTO
{
    [Key]
    public int RoleId { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string RoleName { get; set; } = null!;

    public int? UserId { get; set; }

}
