using Microsoft.AspNetCore.Authentication.Cookies; // BU EKLENDİ
using Microsoft.EntityFrameworkCore;
using NetflixMvcApp.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. MVC Servisleri
builder.Services.AddControllersWithViews();

// 2. Veritabanı Bağlantısı
builder.Services.AddDbContext<NetflixDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 3. GİRİŞ SİSTEMİ (Cookie Authentication) - YENİ EKLENEN KISIM
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Giriş yapmamışsa buraya at
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20); // 20 dk sonra oturum düşsün
    });

var app = builder.Build();

// ... (Buradaki kodlar aynı kalıyor)

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 4. SIRALAMA ÖNEMLİ: Önce Kimlik Doğrulama, Sonra Yetkilendirme
app.UseAuthentication(); // YENİ EKLENDİ
app.UseAuthorization();

// Başlangıç rotası yine Home/Index olsun, ama orası korumalı olacak
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();