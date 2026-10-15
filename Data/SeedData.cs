using Microsoft.EntityFrameworkCore;
using NetflixMvcApp.Models;

namespace NetflixMvcApp.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new NetflixDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<NetflixDbContext>>()))
            {
                context.Database.EnsureCreated();

                // Eğer kullanıcı varsa (daha önce eklediysek) tekrar ekleme
                if (context.Users.Any()) { return; }

                // --- SENİN SQL KODLARIN BURADA ÇALIŞIYOR ---
                // Not: Raw SQL ile verileri ve ilişkileri birebir senin istediğin ID'lerle basıyoruz.
                
                var sql = @"
                    -- Önce temizlik (gerekirse)
                    TRUNCATE TABLE watchhistory, content_genre, genres, content, profiles, users RESTART IDENTITY CASCADE;

                    -- 1. KULLANICILAR
                    INSERT INTO users (user_id, email, password, subscription_type) VALUES
                    (1, 'aleyna@gmail.com', '12345', 'Premium'),
                    (2, 'sinem@hotmail.com', 'sifre1', 'Standard'),
                    (3, 'esra@yahoo.com', 'esra123', 'Basic'),
                    (4, 'aybala@gmail.com', 'aybala_pass', 'Premium'),
                    (5, 'bahadır@outlook.com', 'bahadıer2023', 'Standard'),
                    (6, 'enes@yandex.com', 'enes99', 'Basic'),
                    (7, 'irem@gmail.com', 'i1234', 'Premium'),
                    (8, 'betül@hotmail.com', 'betülpass', 'Standard'),
                    (9, 'haticekübra@gmail.com', 'hkübra_fb', 'Basic'),
                    (10, 'hudoshayrus@gmail.com','hh1234ö', 'Premium'),
                    (11, 'aliyiğit@yahoo.com', 'aliy1234', 'Premium');

                    -- 2. PROFİLLER
                    INSERT INTO profiles (profile_id, user_id, profile_name, is_kid) VALUES
                    (1, 1, 'Aleyna', FALSE), 
                    (2, 1, 'Aleyna Kid', TRUE), 
                    (3, 2, 'Sinem', FALSE), 
                    (4, 3, 'Esra', FALSE),
                    (5, 4, 'Aybala', FALSE), 
                    (6, 5, 'Bahadır', FALSE),
                    (7, 6, 'Enes', FALSE), 
                    (8, 7, 'Irem', FALSE),
                    (9, 8, 'Betul', FALSE), 
                    (10, 9, 'Hatice Kubra', FALSE),
                    (11, 10, 'Huda', FALSE),
                    (12, 11, 'Ali Yigit', FALSE);

                    -- 3. İÇERİKLER (Resim linklerini ekledik ki boş görünmesin)
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

                    -- 4. TÜRLER
                    INSERT INTO genres (genre_id, genre_name) VALUES
                    (1, 'Drama'),(2, 'Crime'),(3, 'Sci-Fi'),(4, 'Thriller'),(5, 'Horror'),(6, 'Comedy'),(7, 'Action'),(8, 'Mystery'),(9, 'Romance'),(10, 'Adventure'); 

                    -- 5. İÇERİK - TÜR
                    INSERT INTO content_genre (content_id, genre_id) VALUES
                    (1, 1), (1, 2), -- The Godfather: Drama, Crime
                    (2, 1), (2, 2), -- Breaking Bad: Drama, Crime
                    (3, 3), (3, 7), -- Inception: Sci-Fi, Action
                    (4, 3), (4, 4), -- Stranger Things: Sci-Fi, Thriller
                    (5, 3), (5, 7), -- The Matrix: Sci-Fi, Action
                    (6, 3), (6, 10),-- Interstellar: Sci-Fi, Adventure
                    (7, 3), (7, 8), -- Dark: Sci-Fi, Mystery
                    (8, 7), (8, 10),-- Avengers: Action, Adventure
                    (9, 6), (9, 9), -- Friends: Comedy, Romance
                    (10, 1), (10, 4);-- Parasite: Drama, Thriller

                    -- 6. İZLEME GEÇMİŞİ
                    INSERT INTO watchhistory (history_id, profile_id, content_id, watched_date, completion_percentage) VALUES
                    (1, 1, 1, '2023-10-01', 100), 
                    (2, 2, 4, '2023-10-02', 20),  
                    (3, 3, 2, '2023-10-05', 90),
                    (4, 4, 3, '2023-10-06', 100),
                    (5, 5, 5, '2023-10-07', 50),
                    (6, 6, 6, '2023-10-08', 100),
                    (7, 7, 7, '2023-10-09', 10),
                    (8, 8, 8, '2023-10-10', 100),
                    (9, 9, 9, '2023-10-11', 100),
                    (10, 10, 10, '2023-10-12', 45),
                    (11, 11, 1, '2023-10-13', 100);

                    -- Sayaçları (ID üreteçlerini) en son ID'ye göre güncelle
                    SELECT setval('users_user_id_seq', (SELECT MAX(user_id) FROM users));
                    SELECT setval('profiles_profile_id_seq', (SELECT MAX(profile_id) FROM profiles));
                    SELECT setval('content_content_id_seq', (SELECT MAX(content_id) FROM content));
                    SELECT setval('genres_genre_id_seq', (SELECT MAX(genre_id) FROM genres));
                    SELECT setval('watchhistory_history_id_seq', (SELECT MAX(history_id) FROM watchhistory));
                ";

                // SQL'i çalıştır
                context.Database.ExecuteSqlRaw(sql);
            }
        }
    }
}