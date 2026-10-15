using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NetflixMvcApp.Data;
using System.Security.Claims;

namespace NetflixMvcApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly NetflixDbContext _context;

        public AccountController(NetflixDbContext context)
        {
            _context = context;
        }

        // 1. GİRİŞ SAYFASINI GÖSTER (GET)
        public IActionResult Login()
        {
            // Eğer zaten giriş yapmışsa Ana Sayfaya at
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // 2. GİRİŞ YAPMA İŞLEMİ (POST)
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            // Veritabanında bu email ve şifreye sahip kullanıcı var mı?
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user != null)
            {
                // Kullanıcı bulundu! Kimlik kartı (Claims) hazırlayalım
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim("UserId", user.UserId.ToString()),
                    new Claim("Subscription", user.SubscriptionType)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties();

                // Sisteme giriş yap (Cookie oluştur)
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme, 
                    new ClaimsPrincipal(claimsIdentity), 
                    authProperties);

                return RedirectToAction("Index", "Home"); // Başarılıysa Home sayfasına git
            }

            // Hatalıysa
            ViewBag.Error = "E-posta adresi veya şifre hatalı!";
            return View();
        }

        // 3. ÇIKIŞ YAP (LOGOUT)
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}