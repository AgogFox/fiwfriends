using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Event.Models
{
    public class User
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public bool Islogin { get; set; }

        public string Picture { get; set; }

        public object Location { get; set; }

        public object Event { get; set; }
    }
}
