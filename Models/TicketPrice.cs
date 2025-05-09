using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace nafCine.Models;

public partial class TicketPrice
{
    [Key]
    public int PriceId { get; set; }

    public int? ScreenId { get; set; }

    public int? ShowtimeId { get; set; }

    public string TicketType { get; set; } = null!;

    public decimal Price { get; set; }

    public virtual Screen? Screen { get; set; }

    public virtual Showtime? Showtime { get; set; }
}
