using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetflixMvcApp.Data;

namespace NetflixMvcApp.Controllers
{
    public class ProfilesController : Controller
    {
        private readonly NetflixDbContext _context;
        public ProfilesController(NetflixDbContext context) { _context = context; }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Profiles.Include(p => p.User).ToListAsync());
        }
    }
}