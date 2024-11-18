using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Keyless]
public partial class ActorsInMoviesView
{
    public int AcInMoId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string ActorFirstname { get; set; } = null!;

    [StringLength(200)]
    [Unicode(false)]
    public string? ActorLastname { get; set; }

    public int MovieId { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string MovieName { get; set; } = null!;

    public bool IsRecordActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    public int? CreatedByUserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastModificationAt { get; set; }

    public int? LastModificationByUserId { get; set; }
}
