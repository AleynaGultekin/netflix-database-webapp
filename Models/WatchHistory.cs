using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetflixMvcApp.Models
{
    public class WatchHistory
    {
        [Key]
        public int HistoryId { get; set; } // SQL'deki history_id

        public int ProfileId { get; set; }
        public Profile? Profile { get; set; }

        public int ContentId { get; set; }
        public Content? Content { get; set; }

        public DateTime WatchedDate { get; set; }

        public int CompletionPercentage { get; set; }
    }
}