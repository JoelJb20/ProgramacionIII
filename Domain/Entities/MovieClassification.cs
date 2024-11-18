using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

public partial class MovieClassification
{
    [Key]
    public int ClassificationId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string ClassificationName { get; set; } = null!;

    public bool IsRecordActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    public int? CreatedByUserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastModificationAt { get; set; }

    public int? LastModificationByUserId { get; set; }

    [InverseProperty("Classification")]
    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
