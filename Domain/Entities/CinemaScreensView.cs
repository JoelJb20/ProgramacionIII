using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Keyless]
public partial class CinemaScreensView
{
    public int ScreenId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? ScreenName { get; set; }

    public int? CinemaId { get; set; }

    [StringLength(300)]
    [Unicode(false)]
    public string? CinemaName { get; set; }

    public int? Seats { get; set; }

    [Column(TypeName = "numeric(12, 2)")]
    public decimal? GeneralPriceBySeat { get; set; }

    public bool IsRecordActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    public int? CreatedByUserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastModificationAt { get; set; }

    public int? LastModificationByUserId { get; set; }
}
