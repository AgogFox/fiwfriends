<<<<<<< HEAD
﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Event.Models
=======
﻿using System.ComponentModel.DataAnnotations;

namespace FiwFriends.Models
>>>>>>> search-result-page
{
    public class EventOBJ
    {
        [Key] public string title { get; set; }
<<<<<<< HEAD
        [Required] 
        public string host_by { get; set; }
        [Required]
        [DisplayName("event time")]
=======
        [Required]
        public string host_by { get; set; }
        [Required]
>>>>>>> search-result-page
        public DateTime date_time { get; set; }
        [Required]
        public string location { get; set; }
        public IFormFile picture { get; set; }
        public string picture_url { get; set; }
        public string description { get; set; }
<<<<<<< HEAD
        [DisplayName("close form at")]
        public DateTime close_form { get; set; }
=======
>>>>>>> search-result-page
        public List<Client> attendees { get; set; }
        [Required]
        public List<string> tags { get; set; }
        public int spots { get; set; }
<<<<<<< HEAD
        public bool is_open {  get; set; }
        public int spots_left()
        { return spots - attendees.Count; }
    }

=======
        public int spots_left()
        { return spots - attendees.Count; }
    }
    public class EventViewModel
    {
        public List<EventOBJ> Eventsearch { get; set; }
        // Add other properties needed in the next view
    }
>>>>>>> search-result-page

    public class Client
    {
        public string username { get; set; }
        public string profile_pic { get; set; }
    }
}
<<<<<<< HEAD
=======

>>>>>>> search-result-page
