using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FiwFriends.Models
{
    public class Usersystem
    {
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; }
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }
        public IFormFile Picture { get; set; }
        public string Picture_url { get; set; }
        public string Location { get; set; }
        public object Event { get; set; }
        public string AboutMe { get; set; }
        public string Interest { get; set; }

    }
}
