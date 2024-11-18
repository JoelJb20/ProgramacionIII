using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

public partial class ActorsInMovie
{
    [Key]
    public int AcInMoId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string ActorFirstname { get; set; } = null!;

    [StringLength(200)]
    [Unicode(false)]
    public string? ActorLastname { get; set; }

    public int MovieId { get; set; }

    public bool IsRecordActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    public int? CreatedByUserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastModificationAt { get; set; }

    public int? LastModificationByUserId { get; set; }

    [ForeignKey("MovieId")]
    [InverseProperty("ActorsInMovies")]
    public virtual Movie Movie { get; set; } = null!;
}
