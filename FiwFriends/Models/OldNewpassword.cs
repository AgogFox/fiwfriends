using System.ComponentModel.DataAnnotations;

namespace FiwFriends.Models
{
    public class OldNewpassword
    {
        [Required]
        [MinLength(6)]
        public string? OldPassword { get; set; }
        [Required]
        [MinLength(6)]
        public string? NewPassword { get; set; }
    }
}
