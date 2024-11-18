using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Application.DTO;

public  class CinemaScreenDTO
{
    [Key]
    public int ScreenId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string ScreenName { get; set; } = null!;

    public int? CinemaId { get; set; }

    public int? Seats { get; set; }

    [Column(TypeName = "numeric(12, 2)")]
    public decimal? GeneralPriceBySeat { get; set; }
    public int? UserId { get; set; }

}
