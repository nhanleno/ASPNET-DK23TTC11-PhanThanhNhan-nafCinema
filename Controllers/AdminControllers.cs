using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using nafCine.Data;
using nafCine.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace nafCine.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Trang chính của Admin (Menu)
        public IActionResult Index()
        {
            ViewBag.MovieCount = _context.Movies.Count();
            ViewBag.CinemaCount = _context.Cinemas.Count();
            ViewBag.ScreenCount = _context.Screens.Count();
            ViewBag.ShowtimeCount = _context.Showtimes.Count();

            return View();
        }

        // === THÊM PHIM ===
        [HttpGet]
        public IActionResult CreateMovie()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMovie(Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Movies.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // === THÊM RẠP CHIẾU ===
        [HttpGet]
        public IActionResult CreateCinema()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCinema(Cinema cinema)
        {
            if (ModelState.IsValid)
            {
                _context.Cinemas.Add(cinema);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(cinema);
        }

        // === THÊM SUẤT CHIẾU ===
        [HttpGet]
        public IActionResult CreateShowtime()
        {
            // Chuẩn bị dữ liệu cho dropdown
            ViewBag.Movies = new SelectList(_context.Movies.ToList(), "MovieId", "Title");
            ViewBag.Screens = new SelectList(_context.Screens.ToList(), "ScreenId", "RoomName");

            // Truyền một instance mới của Showtime để tránh Model null
            var showtime = new Showtime();
            

            return View(showtime);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateShowtime(Showtime showtime)
        {
            if (ModelState.IsValid)
            {
                _context.Showtimes.Add(showtime);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        System.Diagnostics.Debug.WriteLine($"Field: {state.Key} - Error: {error.ErrorMessage}");
                    }
                }
            }

            ViewBag.Movies = new SelectList(_context.Movies.ToList(), "MovieId", "Title");
            ViewBag.Screens = new SelectList(_context.Screens.ToList(), "ScreenId", "RoomName");
            return View(showtime);
        }

        // === THÊM GIÁ VÉ ===
        [HttpGet]
        public IActionResult CreateTicketPrice()
        {
            // Nếu cần: có thể truyền danh sách phòng chiếu/suất chiếu để chọn thông qua dropdown.
            ViewBag.Screens = new SelectList(_context.Screens.ToList(), "ScreenId", "RoomName");
            ViewBag.Showtimes = new SelectList(_context.Showtimes.ToList(), "ShowtimeId", "StartTime");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTicketPrice(TicketPrice ticketPrice)
        {
            if (ModelState.IsValid)
            {
                _context.TicketPrices.Add(ticketPrice);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Screens = new SelectList(_context.Screens.ToList(), "ScreenId", "RoomName");
            ViewBag.Showtimes = new SelectList(_context.Showtimes.ToList(), "ShowtimeId", "StartTime");
            return View(ticketPrice);
        }

     

        [HttpGet]
        public IActionResult CreateScreen()
        {
            // Lấy danh sách Cinemas để truyền vào dropdown
            var cinemas = _context.Cinemas.ToList();
            ViewBag.Cinemas = new SelectList(cinemas, "CinemaId", "CinemaName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateScreen(Screen screen)
        {
            if (ModelState.IsValid)
            {
                // Thêm mới Screen
                _context.Screens.Add(screen);
                int result = await _context.SaveChangesAsync();
                System.Diagnostics.Debug.WriteLine($"SaveChangesAsync result: {result}");

                // Kiểm tra xem người dùng đã nhập số ghế mong muốn chưa
                // Giả sử thuộc tính TotalSeats trong model Screen biểu thị tổng số ghế mong muốn cho phòng chiếu.
                // Nếu không có, bạn có thể dùng số mặc định, ví dụ: 50 (tức 5 hàng x 10 ghế)
                int totalSeats = screen.TotalSeats > 0 ? screen.TotalSeats : 50;
                int seatsPerRow = 10;

                // Tính số hàng cần tạo: làm tròn lên
                int rows = (int)Math.Ceiling(totalSeats / (double)seatsPerRow);

                // Tạo ghế dựa theo số hàng và số ghế trên mỗi hàng
                // Mỗi hàng được đánh số bằng một ký tự, ví dụ hàng đầu tiên là 'A', tiếp theo là 'B', ...
                for (int row = 0; row < rows; row++)
                {
                    char rowLetter = (char)('A' + row);

                    // Xác định số ghế của hàng này:
                    // Nếu là hàng cuối và không đủ ghế chia hết, số ghế = totalSeats % seatsPerRow (nếu bằng 0 thì vẫn là seatsPerRow).
                    int seatsInThisRow = (row == rows - 1 && totalSeats % seatsPerRow != 0)
                                           ? totalSeats % seatsPerRow
                                           : seatsPerRow;

                    for (int seatNumber = 1; seatNumber <= seatsInThisRow; seatNumber++)
                    {
                        var seat = new Seat
                        {
                            ScreenId = screen.ScreenId,  // Liên kết với Screen mới tạo
                            RowNumber = rowLetter.ToString(),
                            SeatNumber = seatNumber,
                            SeatClass = "Regular" // Có thể thay đổi nếu cần phân loại ghế khác nhau
                        };
                        _context.Seats.Add(seat);
                    }
                }
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            else
            {
                // Nếu có lỗi ModelState, ghi log để kiểm tra
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        System.Diagnostics.Debug.WriteLine($"Field: {state.Key} - Error: {error.ErrorMessage}");
                    }
                }
            }

            var cinemas = _context.Cinemas.ToList();
            ViewBag.Cinemas = new SelectList(cinemas, "CinemaId", "CinemaName", screen.CinemaId);
            return View(screen);
        }

     


    }
}