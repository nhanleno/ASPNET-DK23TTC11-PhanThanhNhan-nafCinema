using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nafCine.Data;
using System.Threading.Tasks;

namespace nafCine.Controllers
{
    public class CinemaController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CinemaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Hiển thị danh sách rạp
        public async Task<IActionResult> Index()
        {
            var cinemas = await _context.Cinemas.ToListAsync();
            return View(cinemas);
        }

        // Trang chi tiết từng rạp nếu cần
        public async Task<IActionResult> Details(int id)
        {
            var cinema = await _context.Cinemas.FirstOrDefaultAsync(c => c.CinemaId == id);
            if (cinema == null)
            {
                return NotFound();
            }
            return View(cinema);
        }
    }
}