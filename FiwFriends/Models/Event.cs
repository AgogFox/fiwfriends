using System.ComponentModel.DataAnnotations;
namespace Test.Models
{
    public class Event
    {
        public class EventOBJ
        {
            [Key] public string title { get; set; }
            [Required]
            public string host_by { get; set; }
            [Required]
            public DateTime date_time { get; set; }
            [Required]
            public string location { get; set; }
            public IFormFile picture { get; set; }
            public string picture_url { get; set; }
            public string description { get; set; }
            public List<Client> attendees { get; set; }
            [Required]
            public List<string> tags { get; set; }
            public int spots { get; set; }
            public int spots_left()
            { return spots - attendees.Count; }
        }

        public class ListofEvent {
            public List<EventOBJ> events { get; set; }
        }
            public class Client
            {
                public string username { get; set; }
                public string profile_pic { get; set; }
            }
        
    }
}
