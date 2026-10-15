using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetflixMvcApp.Data;
using NetflixMvcApp.Models; // Content modeli için

namespace NetflixMvcApp.Controllers
{
    public class ContentController : Controller
    {
        private readonly NetflixDbContext _context;
        public ContentController(NetflixDbContext context) { _context = context; }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Contents.ToListAsync());
        }
        
        // Create metodları (daha önce eklediysek kalabilir, yoksa burayı da ekle)
        public IActionResult Create() => View();
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Content content)
        {
            if (ModelState.IsValid) { _context.Add(content); await _context.SaveChangesAsync(); return RedirectToAction(nameof(Index)); }
            return View(content);
        }
    }
}