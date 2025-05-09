using System;
using System.Collections.Generic;

namespace nafCine.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public int UserId { get; set; }

    public int ShowtimeId { get; set; }

    public DateTime BookingDate { get; set; }

    public decimal TotalAmount { get; set; }

    public virtual ICollection<BookedSeat> BookedSeats { get; set; } = new List<BookedSeat>();

    public virtual Showtime Showtime { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
