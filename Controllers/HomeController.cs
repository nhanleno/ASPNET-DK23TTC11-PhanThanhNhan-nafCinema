using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nafCine.Data;
using nafCine.Models;
using System.Threading.Tasks;

namespace nafCine.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Trang chủ: Hiển thị phim mới và phim đang chiếu
        public async Task<IActionResult> Index()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            ViewBag.TestUserId = userId;

            var movies = await _context.Movies.ToListAsync();
            return View(movies);
        }

    }
}