using System;
using System.Collections.Generic;

namespace nafCine.Models;

public partial class Cinema
{
    public int CinemaId { get; set; }

    public string CinemaName { get; set; } = null!;

    public string? Address { get; set; }

    public string? ContactInfo { get; set; }

    public string? MapLocation { get; set; }

    public virtual ICollection<Screen> Screens { get; set; } = new List<Screen>();
}
