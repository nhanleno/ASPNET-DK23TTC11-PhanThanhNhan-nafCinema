using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace nafCine.Models;

public partial class Screen
{
    public int ScreenId { get; set; }

    public int? CinemaId { get; set; }

    public string RoomName { get; set; } = null!;

    public int TotalSeats { get; set; }
    
    [ValidateNever]
    public virtual Cinema Cinema { get; set; } = null!;

    public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();

    public virtual ICollection<Showtime> Showtimes { get; set; } = new List<Showtime>();

    public virtual ICollection<TicketPrice> TicketPrices { get; set; } = new List<TicketPrice>();
}
