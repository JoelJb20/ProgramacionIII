using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

public partial class MovieGender
{
    [Key]
    public int GenderId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string GenderName { get; set; } = null!;

    public bool IsRecordActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    public int? CreatedByUserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastModificationAt { get; set; }

    public int? LastModificationByUserId { get; set; }

    [InverseProperty("Gender")]
    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
