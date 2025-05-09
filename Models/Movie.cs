using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace nafCine.Models;

public partial class Movie
{
    public int MovieId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? Director { get; set; }

    public string? Actors { get; set; }

    public int? Duration { get; set; }

    public DateOnly? ReleaseDate { get; set; }

    public string? Genre { get; set; }

    public string? TrailerUrl { get; set; }

    public string? PosterImageUrl { get; set; }

    public string? AgeRating { get; set; }

    public virtual ICollection<Showtime> Showtimes { get; set; } = new List<Showtime>();
}
