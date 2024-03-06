using Microsoft.AspNetCore.Components.Routing;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public string Location {get;set;}

        public List<EventOBJ> Event { get; set; }

        public string AboutMe { get; set; }
    }
}
