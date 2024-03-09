
﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

    namespace FiwFriends.Models
    {
        public class EventOBJ
        {
            [Key] public string title { get; set; }

            [Required]
            public string host_by { get; set; }

            [Required]
            [DisplayName("event time")]
            public DateTime date_time { get; set; }

            [Required]
            public string location { get; set; }
            public IFormFile picture { get; set; }
            public string picture_url { get; set; }
            public string description { get; set; }
            [DisplayName("close form at")]
            public DateTime close_form { get; set; }
            public List<int> attendees { get; set; }
            [Required]
            public List<string> tags { get; set; }
            public int spots { get; set; }
            public bool is_open { get; set; }
            public int time_left()
            { 
                DateTime date = close_form.Date;
                DateTime today = DateTime.Now;
                TimeSpan differ = date.Subtract(today);
                if (differ.Days < 0)
                {
                    is_open = false;
                }
            return differ.Days;
            }
            public int spots_left()
            {
                var left = spots - attendees.Count;
                if (left == 0)
                {
                    is_open=false;
                }
                return attendees.Count; 
            }
        }
        public class EventViewModel
        {
            public List<EventOBJ> Eventsearch { get; set; }
            // Add other properties needed in the next view
        }
}