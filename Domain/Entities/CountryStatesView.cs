using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Keyless]
public partial class CountryStatesView
{
    public int StateId { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string StateName { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? StateCode { get; set; }

    public int? CountryId { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? CountryName { get; set; }

    public bool IsRecordActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    public int? CreatedByUserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastModificationAt { get; set; }

    public int? LastModificationByUserId { get; set; }
}
