using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class SignUpDTO
    {
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
        [StringLength(200)]
        [Unicode(false)]
        public string? Firstname { get; set; }
        [StringLength(200)]
        [Unicode(false)]
        public string? Lastname { get; set; }

        [Required]
        public int? RoleId { get; set; }
        public int? CreatorUserId { get; set; }
    }
}
