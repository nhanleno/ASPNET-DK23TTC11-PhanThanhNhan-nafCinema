using Microsoft.EntityFrameworkCore;
using nafCine.Models;

namespace nafCine.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Screen> Screens { get; set; }
        public DbSet<Showtime> Showtimes { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<TicketPrice> TicketPrices { get; set; }
        // Nếu dùng ASP.NET Identity, có thể tạo DbSet cho IdentityUser
        public DbSet<User> Users { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookedSeat> BookedSeats { get; set; }
    }
}