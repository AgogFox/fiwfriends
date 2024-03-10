using System.ComponentModel.DataAnnotations;

namespace FiwFriends.Models
{
    public class Usersystem
    {
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
        public string Picture { get; set; }
        public object Location { get; set; }
        public object Event { get; set; }
        public string AboutMe { get; set; }
        public string Interest { get; set; }

    }
}
