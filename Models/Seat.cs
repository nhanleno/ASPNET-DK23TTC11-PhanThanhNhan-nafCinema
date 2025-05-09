using System;
using System.Collections.Generic;

namespace nafCine.Models;

public partial class Seat
{
    public int SeatId { get; set; }

    public int ScreenId { get; set; }

    public string RowNumber { get; set; } = null!;

    public int SeatNumber { get; set; }

    public string SeatClass { get; set; } = null!;

    public virtual ICollection<BookedSeat> BookedSeats { get; set; } = new List<BookedSeat>();

    public virtual Screen Screen { get; set; } = null!;
}
