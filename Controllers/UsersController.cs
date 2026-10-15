using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetflixMvcApp.Data;

namespace NetflixMvcApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly NetflixDbContext _context;
        public UsersController(NetflixDbContext context) { _context = context; }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }
    }
}