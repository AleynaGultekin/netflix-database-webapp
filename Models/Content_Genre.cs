using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetflixMvcApp.Models
{
    public class ContentGenre
    {
        // Burada "Id" propertysi YOK çünkü SQL tablosunda yok.
        
        public int ContentId { get; set; }
        public Content? Content { get; set; }

        public int GenreId { get; set; }
        public Genre? Genre { get; set; }
    }
}