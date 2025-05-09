using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nafCine.Data;
using nafCine.Models;
using nafCine.ViewModels;
using System.Security.Claims;
using System.Threading.Tasks;

namespace nafCine.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;
        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Hiển thị trang chọn suất chiếu của phim
        #region SelectShowtime
        
        public IActionResult SelectShowtime(int movieId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


            if (userId == null)
            {
                TempData["RedirectAfterLogin"] = Url.Action("SelectShowtime", "Booking", new { movieId });
                return RedirectToAction("Login", "Account");
            }



            // Nếu đã đăng nhập, tiếp tục chọn rạp
            var showtimes = _context.Showtimes
                .Where(s => s.MovieId == movieId)
                .Include(s => s.Screen)
                     .ThenInclude(screen => screen.Cinema)
                .ToList();


            return View(showtimes);
        }
        #endregion

        // Trang đặt ghế
        #region SelectSeats
        [HttpGet]
        public async Task<IActionResult> SelectSeats(int showtimeId)
        {
            if (showtimeId <= 0)
            {
                return NotFound(); // Nếu không có showtimeId hợp lệ, trả về lỗi
            }

            // Lấy thông tin suất chiếu để biết được phòng chiếu
            var showtime = await _context.Showtimes
                .Include(s => s.Screen)
                .FirstOrDefaultAsync(s => s.ShowtimeId == showtimeId);
            if (showtime == null)
            {
                return NotFound();
            }

            // Lấy danh sách ghế của phòng chiếu hiện tại
            var seats = await _context.Seats
                .Where(s => s.ScreenId == showtime.ScreenId)
                .Include(s => s.BookedSeats)
                    .ThenInclude(bs => bs.Booking)
                .ToListAsync();

            // Lấy danh sách các ghế đã đặt qua bảng BookedSeats
            var bookedSeatIds = await _context.BookedSeats
                .Where(bs => bs.Booking.ShowtimeId == showtimeId)
                .Select(bs => bs.SeatId)
                .ToListAsync();

            // Chuyển đổi danh sách Seat thành SeatViewModel
            var seatViewModels = seats.Select(s => new SeatViewModel
            {
                SeatId = s.SeatId,
                RowNumber = s.RowNumber, // Gán đúng thông tin từ Model
                SeatNumber = s.SeatNumber, // Gán đúng thông tin từ Model
                IsBooked = bookedSeatIds.Contains(s.SeatId)
            }).ToList();

            // Xác định số hàng và số cột (đây chỉ là ví dụ)
            int rows = 5;
            int columns = seatViewModels.Count / rows;
            if (rows * columns < seatViewModels.Count)
            {
                columns++;
            }

            // Tạo đối tượng SeatSelectionViewModel
            var viewModel = new SeatSelectionViewModel
            {
                Rows = rows,
                Columns = columns,
                Seats = seatViewModels,
                ShowtimeId = showtimeId
            };

            ViewBag.Showtime = showtime; // Nếu bạn cần truyền thêm thông tin về suất chiếu
            return View(viewModel);
        }
        #endregion


        // Xử lý thanh toán và đặt vé (đơn giản)
        #region Checkout
        [HttpPost]
        public async Task<IActionResult> Checkout(int showtimeId, int[] selectedSeatIds)
        {
            if (selectedSeatIds == null || selectedSeatIds.Length == 0)
            {
                TempData["ErrorMessage"] = "Bạn phải chọn ít nhất 1 ghế.";
                return RedirectToAction("SelectSeats", new { showtimeId });
            }

            // Kiểm tra nếu người dùng đã đăng nhập
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //var userIdString = HttpContext.Session.GetString("UserId");
            Console.WriteLine($"UserId khi đặt vé: {userIdString}");

            if (string.IsNullOrEmpty(userIdString))
            {
                
                TempData["RedirectAfterLogin"] = Url.Action("Checkout", "Booking", new { showtimeId, selectedSeatIds });
                return RedirectToAction("Login", "Account");
            }

            int userId = int.Parse(userIdString);


            var booking = new Booking
            {
                UserId = userId,
                ShowtimeId = showtimeId,
                BookingDate = DateTime.Now,
                TotalAmount = selectedSeatIds.Length * 100000
            };
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            foreach (var seatId in selectedSeatIds)
            {
                var bookedSeat = new BookedSeat
                {
                    BookingId = booking.BookingId,
                    SeatId = seatId
                };
                _context.BookedSeats.Add(bookedSeat);
            }
            await _context.SaveChangesAsync();

            return RedirectToAction("Confirmation", new { bookingId = booking.BookingId });
        }
        #endregion

        #region Confirmation
        public async Task<IActionResult> Confirmation(int bookingId)
        {
            var booking = await _context.Bookings
            .Include(b => b.Showtime)
                .ThenInclude(s => s.Movie)
            .Include(b => b.Showtime)
                .ThenInclude(s => s.Screen.Cinema)
            .Include(b => b.BookedSeats)
                .ThenInclude(bs => bs.Seat)  // Đảm bảo load dữ liệu từ bảng Seats
            .FirstOrDefaultAsync(b => b.BookingId == bookingId);

            if (booking == null)
                return NotFound();

            return View(booking);
        }
        #endregion



    }
}