using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.DTO;

public class MovieDTO
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

    public int? UserId { get; set; }
    public List<ActorsInMovieDTO> ActorsInMovies { get; set; } = new List<ActorsInMovieDTO>();
}

public class MovieImageDTO
{
    public int MovieId { get; set; }
    public int? UserId { get; set; }
    public IFormFile? ImageUploaded { get; set; }

}
