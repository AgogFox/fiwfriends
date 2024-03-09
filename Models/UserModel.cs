using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FiwFriends.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [DisplayName("Profile Picture")]
        public string? ProfilePictureId { get; set; }
        public List<string> UserImages { get; set; } = new List<string>();
        public string? Location { get; set; }
        [DisplayName("About Me")]
        public string? AboutMe { get; set; }
        public List<int> AttendingEventsId { get; set; } = new List<int>();
        public List<int> HostedEventsId { get; set; } = new List<int>();
    }
}
