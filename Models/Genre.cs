using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace NetflixMvcApp.Models
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }

        [Required]
        public string GenreName { get; set; } = string.Empty;

        // DİKKAT: Burası eskiden Content_Genre idi, şimdi ContentGenre oldu
        public ICollection<ContentGenre> ContentGenres { get; set; } = new List<ContentGenre>();
    }
}