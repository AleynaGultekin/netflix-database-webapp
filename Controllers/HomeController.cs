using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using NetflixMvcApp.Data;
using NetflixMvcApp.Models;
using System.Diagnostics;

namespace NetflixMvcApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly NetflixDbContext _context;

        public HomeController(NetflixDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // BU LİNKE TIKLAYINCA SENİN SQL KODLARIN ÇALIŞACAK
        public IActionResult Setup()
        {
            try
            {
                // 1. Veritabanı yoksa oluştur (Migrations olmasa bile tabloları oluşturur)
                _context.Database.EnsureCreated();

                // 2. SENİN HAZIRLADIĞIN SQL KODLARI
                var sql = @"
                    TRUNCATE TABLE watchhistory, content_genre, genres, content, profiles, users RESTART IDENTITY CASCADE;

                    INSERT INTO users (user_id, email, password, subscription_type, created_at) VALUES
                    (1, 'aleyna@gmail.com', '12345', 'Premium', NOW()),
                    (2, 'sinem@hotmail.com', 'sifre1', 'Standard', NOW()),
                    (3, 'esra@yahoo.com', 'esra123', 'Basic', NOW()),
                    (4, 'aybala@gmail.com', 'aybala_pass', 'Premium', NOW()),
                    (5, 'bahadır@outlook.com', 'bahadıer2023', 'Standard', NOW()),
                    (6, 'enes@yandex.com', 'enes99', 'Basic', NOW()),
                    (7, 'irem@gmail.com', 'i1234', 'Premium', NOW()),
                    (8, 'betül@hotmail.com', 'betülpass', 'Standard', NOW()),
                    (9, 'haticekübra@gmail.com', 'hkübra_fb', 'Basic', NOW()),
                    (10, 'hudoshayrus@gmail.com','hh1234ö', 'Premium', NOW()),
                    (11, 'aliyiğit@yahoo.com', 'aliy1234', 'Premium', NOW());

                    INSERT INTO profiles (profile_id, user_id, profile_name, is_kid) VALUES
                    (1, 1, 'Aleyna', FALSE), (2, 1, 'Aleyna Kid', TRUE), 
                    (3, 2, 'Sinem', FALSE), (4, 3, 'Esra', FALSE),
                    (5, 4, 'Aybala', FALSE), (6, 5, 'Bahadır', FALSE),
                    (7, 6, 'Enes', FALSE), (8, 7, 'Irem', FALSE),
                    (9, 8, 'Betul', FALSE), (10, 9, 'Hatice Kubra', FALSE),
                    (11, 10, 'Huda', FALSE), (12, 11, 'Ali Yigit', FALSE);

                    INSERT INTO content (content_id, title, release_year, duration_minutes, content_type, rating, image_url) VALUES
                    (1, 'The Godfather', 1972, 175, 'Movie', '9.2', 'https://m.media-amazon.com/images/M/MV5BM2MyNjYxNmUtYTAwNi00MTYxLWJmNWYtYzZlODY3ZTk3OTFlXkEyXkFqcGdeQXVyNzkwMjQ5NzM@@._V1_.jpg'),
                    (2, 'Breaking Bad', 2008, 45, 'Series', '9.5', 'https://m.media-amazon.com/images/M/MV5BYmQ4YWMxYjUtNjZmYi00MDQ1LWFhMjMtNjIxZDU5ZDJlNzMwXkEyXkFqcGdeQXVyMTEyMjM2NDc2._V1_.jpg'),
                    (3, 'Inception', 2010, 148, 'Movie', '8.8', 'https://m.media-amazon.com/images/M/MV5BMjAxMzY3NjcxNF5BMl5BanBnXkFtZTcwNTI5OTM0Mw@@._V1_.jpg'),
                    (4, 'Stranger Things', 2016, 50, 'Series', '8.7', 'https://m.media-amazon.com/images/M/MV5BMDZkYmVhNjMtNWU4MC00MDQxLWE3MjYtZGMzZWI1ZjhlOWJmXkEyXkFqcGdeQXVyMTkxNjUyNQ@@._V1_.jpg'),
                    (5, 'The Matrix', 1999, 136, 'Movie', '8.7', 'https://m.media-amazon.com/images/M/MV5BNzQzOTk3OTAtNDQ0Zi00ZTVkLWI0MTEtMDllZjNkYzNjNTc4L2ltYWdlXkEyXkFqcGdeQXVyNjU0OTQ0OTY@._V1_.jpg'),
                    (6, 'Interstellar', 2014, 169, 'Movie', '8.6', 'https://m.media-amazon.com/images/M/MV5BZjdkOTU3MDktN2IxOS00OGEyLWFmMjktY2FiMmZkNWIyODZiXkEyXkFqcGdeQXVyMTMxODk2OTU@._V1_.jpg'),
                    (7, 'Dark', 2017, 60, 'Series', '8.8', 'https://m.media-amazon.com/images/M/MV5BOTk2NzUyOTctZDdlMS00MDJlLTg3MTEtRbTNkYzk1ZjZkYmYwXkEyXkFqcGdeQXVyNzI1NzMxNzM@._V1_.jpg'),
                    (8, 'Avengers: Endgame', 2019, 181, 'Movie', '8.4', 'https://m.media-amazon.com/images/M/MV5BMTc5MDE2ODcwNV5BMl5BanBnXkFtZTgwMzI2NzQ2NzM@._V1_.jpg'),
                    (9, 'Friends', 1994, 22, 'Series', '8.9', 'https://m.media-amazon.com/images/M/MV5BNDVkYjU0MzctMzg0MC00NzQ4LTgxZmEtZDMwNWJlNzAzYzQ2XkEyXkFqcGdeQXVyODUxOTU0OTg@._V1_.jpg'),
                    (10, 'Parasite', 2019, 132, 'Movie', '8.6', 'https://m.media-amazon.com/images/M/MV5BYWZjMjk3ZTItODQ2ZC00NTY5LWE0ZDYtZTI3MjcwN2Q5NTVkXkEyXkFqcGdeQXVyODk4OTc3MTY@._V1_.jpg');

                    -- İD SAYAÇLARINI DÜZELT
                    SELECT setval('users_user_id_seq', (SELECT MAX(user_id) FROM users));
                    SELECT setval('profiles_profile_id_seq', (SELECT MAX(profile_id) FROM profiles));
                    SELECT setval('content_content_id_seq', (SELECT MAX(content_id) FROM content));
                ";

                _context.Database.ExecuteSqlRaw(sql);
                return Content("✅ İŞLEM BAŞARILI! Veriler SQL'den yüklendi. Şimdi menüleri gezebilirsin.");
            }
            catch (Exception ex)
            {
                return Content("❌ HATA: " + ex.Message + "\n" + ex.InnerException?.Message);
            }
        }
    }
}