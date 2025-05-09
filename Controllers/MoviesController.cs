using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nafCine.Data;
using nafCine.Models;
using System.Threading.Tasks;
using System.Linq;

namespace nafCine.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Action hiển thị danh sách phim
        public async Task<IActionResult> Index()
        {
            var movies = await _context.Movies.ToListAsync();
            return View(movies);
        }

        // Action hiển thị chi tiết 1 phim
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.MovieId == id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }
    }
}