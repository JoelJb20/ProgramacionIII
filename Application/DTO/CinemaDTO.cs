using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Application.DTO;
public class CinemaDTO
{
    [Key]
    public int CinemaId { get; set; }

    [StringLength(300)]
    [Unicode(false)]
    public string? CinemaName { get; set; }

    public int? CountryStateId { get; set; }

    [StringLength(300)]
    [Unicode(false)]
    public string? PrimaryAddress { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? PhoneNumber { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Email { get; set; }

    public double? LocationLatitude { get; set; }

    public double? LocationLongitude { get; set; }

    public int? UserId { get; set; }

  
}
