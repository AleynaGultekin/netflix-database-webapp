using Microsoft.EntityFrameworkCore;
using NetflixMvcApp.Models;

namespace NetflixMvcApp.Data
{
    public class NetflixDbContext : DbContext
    {
        public NetflixDbContext(DbContextOptions<NetflixDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<WatchHistory> WatchHistories { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<ContentGenre> ContentGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1. KULLANICILAR (users)
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.Email).HasColumnName("email");
                entity.Property(e => e.Password).HasColumnName("password");
                entity.Property(e => e.SubscriptionType).HasColumnName("subscription_type");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            });

            // 2. PROFİLLER (profiles)
            modelBuilder.Entity<Profile>(entity =>
            {
                entity.ToTable("profiles");
                entity.Property(e => e.ProfileId).HasColumnName("profile_id");
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.ProfileName).HasColumnName("profile_name");
                entity.Property(e => e.IsKid).HasColumnName("is_kid");
            });

            // 3. İÇERİK (content) -> BURASI EKSİKTİ, DÜZELTİLDİ
            modelBuilder.Entity<Content>(entity =>
            {
                entity.ToTable("content");
                entity.Property(e => e.ContentId).HasColumnName("content_id");
                // SQL kodunla eşleşmesi için şunları ekledik:
                entity.Property(e => e.Title).HasColumnName("title");
                entity.Property(e => e.ReleaseYear).HasColumnName("release_year");
                entity.Property(e => e.DurationMinutes).HasColumnName("duration_minutes");
                entity.Property(e => e.ContentType).HasColumnName("content_type");
                entity.Property(e => e.Rating).HasColumnName("rating");
                entity.Property(e => e.ImageUrl).HasColumnName("image_url");
            });

            // 4. TÜRLER (genres) -> BURASI EKSİKTİ, DÜZELTİLDİ
            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("genres");
                entity.Property(e => e.GenreId).HasColumnName("genre_id");
                entity.Property(e => e.GenreName).HasColumnName("genre_name");
            });

            // 5. İÇERİK-TÜR İLİŞKİSİ (content_genre)
            modelBuilder.Entity<ContentGenre>(entity =>
            {
                entity.ToTable("content_genre");
                entity.HasKey(cg => new { cg.ContentId, cg.GenreId });

                entity.Property(e => e.ContentId).HasColumnName("content_id");
                entity.Property(e => e.GenreId).HasColumnName("genre_id");
            });

            // 6. İZLEME GEÇMİŞİ (watchhistory) -> BURASI EKSİKTİ, DÜZELTİLDİ
            modelBuilder.Entity<WatchHistory>(entity =>
            {
                entity.ToTable("watchhistory");
                entity.HasKey(e => e.HistoryId);
                
                entity.Property(e => e.HistoryId).HasColumnName("history_id");
                entity.Property(e => e.ProfileId).HasColumnName("profile_id");
                entity.Property(e => e.ContentId).HasColumnName("content_id");
                entity.Property(e => e.WatchedDate).HasColumnName("watched_date");
                entity.Property(e => e.CompletionPercentage).HasColumnName("completion_percentage");
            });
        }
    }
}