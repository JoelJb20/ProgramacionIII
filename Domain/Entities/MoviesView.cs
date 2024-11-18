using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Keyless]
public partial class MoviesView
{
    public int MovieId { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string MovieName { get; set; } = null!;

    public int? GenderId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? GenderName { get; set; }

    public int? ClassificationId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? ClassificationName { get; set; }

    [StringLength(1500)]
    [Unicode(false)]
    public string? Synopsis { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? DirectorName { get; set; }

    [Column(TypeName = "date")]
    public DateTime? ReleaseDate { get; set; }

    public TimeSpan? ReleaseHour { get; set; }

    [StringLength(18)]
    [Unicode(false)]
    public string MovieTag { get; set; } = null!;

    [Unicode(false)]
    public string? ImageBytes { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? ImageName { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? ImageExtension { get; set; }

    public bool IsRecordActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    public int? CreatedByUserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastModificationAt { get; set; }

    public int? LastModificationByUserId { get; set; }
}
