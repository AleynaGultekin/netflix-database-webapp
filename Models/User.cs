using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetflixMvcApp.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("subscription_type")]
        public string SubscriptionType { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        // İlişkiler
        public ICollection<Profile> Profiles { get; set; }
    }
}