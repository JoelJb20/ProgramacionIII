using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Application.DTO;

public class ActorsInMovieDTO
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

    public int? UserId { get; set; }
}
