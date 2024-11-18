using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

public partial class CinemaScreen
{
    [Key]
    public int ScreenId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? ScreenName { get; set; }

    public int? CinemaId { get; set; }

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

    [ForeignKey("CinemaId")]
    [InverseProperty("CinemaScreens")]
    public virtual Cinema? Cinema { get; set; }

    [InverseProperty("Screen")]
    public virtual ICollection<MoviesByScreen> MoviesByScreens { get; set; } = new List<MoviesByScreen>();
}
