using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetflixMvcApp.Data;

namespace NetflixMvcApp.Controllers
{
    public class WatchHistoryController : Controller
    {
        private readonly NetflixDbContext _context;

        public WatchHistoryController(NetflixDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Veritabanından İzleme Geçmişini + Profil Adını + Film Adını getiriyoruz
            var history = await _context.WatchHistories
                .Include(w => w.Profile)
                .Include(w => w.Content)
                .ToListAsync();

            return View(history);
        }
    }
}