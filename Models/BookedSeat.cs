using System;
using System.Collections.Generic;

namespace nafCine.Models;

public partial class BookedSeat
{
    public int BookedSeatId { get; set; }

    public int BookingId { get; set; }

    public int SeatId { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public virtual Seat Seat { get; set; } = null!;
}
