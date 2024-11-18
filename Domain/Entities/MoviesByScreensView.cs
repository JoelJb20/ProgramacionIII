using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Keyless]
public partial class MoviesByScreensView
{
    public int MovieByScreenId { get; set; }

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

    [StringLength(18)]
    [Unicode(false)]
    public string MovieTag { get; set; } = null!;

    public int ScreenId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? ScreenName { get; set; }

    public int? CinemaId { get; set; }

    [StringLength(300)]
    [Unicode(false)]
    public string? CinemaName { get; set; }

    [Column(TypeName = "date")]
    public DateTime? ShowingDate { get; set; }

    public TimeSpan? ShowingHour { get; set; }

    [Column(TypeName = "numeric(12, 2)")]
    public decimal? PriceBySeat { get; set; }

    public bool? IsHoliday { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? HolidayName { get; set; }

    public bool IsRecordActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    public int? CreatedByUserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastModificationAt { get; set; }

    public int? LastModificationByUserId { get; set; }
}
