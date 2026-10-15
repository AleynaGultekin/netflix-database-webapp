using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetflixMvcApp.Models
{
    [Table("profiles")]
    public class Profile
    {
        [Key]
        [Column("profile_id")]
        public int ProfileId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }
        public User User { get; set; }

        [Column("profile_name")]
        public string ProfileName { get; set; }

        [Column("is_kid")]
        public bool IsKid { get; set; }

        // İlişkiler
        public ICollection<WatchHistory> WatchHistories { get; set; }
    }
}