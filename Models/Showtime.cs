using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace nafCine.Models;

public partial class Showtime
{
    public int ShowtimeId { get; set; }

    public int MovieId { get; set; }

    public int ScreenId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }
    //public int CinemaId { get; set; }
    //public virtual required Cinema Cinema { get; set; }
    
    [ValidateNever]
    public virtual Movie Movie { get; set; } = null!;
    [ValidateNever]
    public virtual Screen Screen { get; set; } = null!;
    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    public virtual ICollection<TicketPrice> TicketPrices { get; set; } = new List<TicketPrice>();
}
