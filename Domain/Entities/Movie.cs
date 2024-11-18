using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

public partial class Movie
{
    [Key]
    public int MovieId { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string MovieName { get; set; } = null!;

    public int? GenderId { get; set; }

    public int? ClassificationId { get; set; }

    [StringLength(1500)]
    [Unicode(false)]
    public string? Synopsis { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? DirectorName { get; set; }

    [Column(TypeName = "date")]
    public DateTime? ReleaseDate { get; set; }

    public TimeSpan? ReleaseHour { get; set; }

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

    [InverseProperty("Movie")]
    public virtual ICollection<ActorsInMovie> ActorsInMovies { get; set; } = new List<ActorsInMovie>();

    [ForeignKey("ClassificationId")]
    [InverseProperty("Movies")]
    public virtual MovieClassification? Classification { get; set; }

    [ForeignKey("GenderId")]
    [InverseProperty("Movies")]
    public virtual MovieGender? Gender { get; set; }

    [InverseProperty("Movie")]
    public virtual ICollection<MoviesByScreen> MoviesByScreens { get; set; } = new List<MoviesByScreen>();
}
