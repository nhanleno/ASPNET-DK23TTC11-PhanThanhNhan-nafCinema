using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace nafCine.Models;

public partial class NafCinemaContext : DbContext
{
    public NafCinemaContext()
    {
    }

    public NafCinemaContext(DbContextOptions<NafCinemaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BookedSeat> BookedSeats { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Cinema> Cinemas { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<Screen> Screens { get; set; }

    public virtual DbSet<Seat> Seats { get; set; }

    public virtual DbSet<Showtime> Showtimes { get; set; }

    public virtual DbSet<TicketPrice> TicketPrices { get; set; }

    public virtual DbSet<User> Users { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookedSeat>(entity =>
        {
            entity.HasKey(e => e.BookedSeatId).HasName("PK__BookedSe__7508A7384376F859");

            entity.HasOne(d => d.Booking).WithMany(p => p.BookedSeats)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookedSea__Booki__5165187F");

            entity.HasOne(d => d.Seat).WithMany(p => p.BookedSeats)
                .HasForeignKey(d => d.SeatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookedSea__SeatI__52593CB8");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Bookings__73951AED27801F8A");

            entity.Property(e => e.BookingDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Showtime).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.ShowtimeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bookings__Showti__4E88ABD4");

            entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bookings__UserId__4D94879B");
        });

        modelBuilder.Entity<Cinema>(entity =>
        {
            entity.HasKey(e => e.CinemaId).HasName("PK__Cinemas__59C92646252328B8");

            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.CinemaName).HasMaxLength(255);
            entity.Property(e => e.ContactInfo).HasMaxLength(255);
            entity.Property(e => e.MapLocation).HasMaxLength(500);
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("PK__Movies__4BD2941A0A82F47D");

            entity.Property(e => e.Actors).HasMaxLength(255);
            entity.Property(e => e.AgeRating).HasMaxLength(10);
            entity.Property(e => e.Director).HasMaxLength(255);
            entity.Property(e => e.Genre).HasMaxLength(100);
            entity.Property(e => e.PosterImageUrl)
                .HasMaxLength(500)
                .HasColumnName("PosterImageURL");
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.TrailerUrl)
                .HasMaxLength(500)
                .HasColumnName("TrailerURL");
        });

        modelBuilder.Entity<Screen>(entity =>
        {
            entity.HasKey(e => e.ScreenId).HasName("PK__Screens__0AB60FA587A0DFB9");

            entity.Property(e => e.RoomName).HasMaxLength(100);

            entity.HasOne(d => d.Cinema).WithMany(p => p.Screens)
                .HasForeignKey(d => d.CinemaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Screens__CinemaI__3B75D760");
        });

        modelBuilder.Entity<Seat>(entity =>
        {
            entity.HasKey(e => e.SeatId).HasName("PK__Seats__311713F3FCF1E394");

            entity.Property(e => e.RowNumber).HasMaxLength(10);
            entity.Property(e => e.SeatClass).HasMaxLength(50);

            entity.HasOne(d => d.Screen).WithMany(p => p.Seats)
                .HasForeignKey(d => d.ScreenId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Seats__ScreenId__4222D4EF");
        });

        modelBuilder.Entity<Showtime>(entity =>
        {
            entity.HasKey(e => e.ShowtimeId).HasName("PK__Showtime__32D31F208DEA64D3");

            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.StartTime).HasColumnType("datetime");

            entity.HasOne(d => d.Movie).WithMany(p => p.Showtimes)
                .HasForeignKey(d => d.MovieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Showtimes__Movie__3E52440B");

            entity.HasOne(d => d.Screen).WithMany(p => p.Showtimes)
                .HasForeignKey(d => d.ScreenId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Showtimes__Scree__3F466844");

        });

        modelBuilder.Entity<TicketPrice>(entity =>
        {
            entity.HasKey(e => e.PriceId).HasName("PK__TicketPr__49575BAFC1D2FBAB");

            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TicketType).HasMaxLength(100);

            entity.HasOne(d => d.Screen).WithMany(p => p.TicketPrices)
                .HasForeignKey(d => d.ScreenId)
                .HasConstraintName("FK__TicketPri__Scree__44FF419A");

            entity.HasOne(d => d.Showtime).WithMany(p => p.TicketPrices)
                .HasForeignKey(d => d.ShowtimeId)
                .HasConstraintName("FK__TicketPri__Showt__45F365D3");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C0A1134A2");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4072F0498").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053480DFE05F").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.PasswordHash).HasMaxLength(500);
            entity.Property(e => e.Username).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
