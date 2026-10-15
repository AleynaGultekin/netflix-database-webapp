using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace NetflixMvcApp.Models
{
    public class Content
    {
        [Key]
        public int ContentId { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string ContentType { get; set; } = string.Empty; // "Movie" veya "Series"

        public int ReleaseYear { get; set; }

        public string Rating { get; set; } = string.Empty; // IMDB Puanı

        public int DurationMinutes { get; set; }

        // Beklenen yeni özellik:
        public string? ImageUrl { get; set; }

        // İlişkiler
        // Eğer ContentGenre hatası alıyorsan, aşağıdaki sınıf adının projedekiyle birebir aynı olduğundan emin olmalıyız.
        public ICollection<ContentGenre> ContentGenres { get; set; } = new List<ContentGenre>();
        
        public ICollection<WatchHistory> WatchHistories { get; set; } = new List<WatchHistory>();
    }
}