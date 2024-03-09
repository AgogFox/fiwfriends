using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FiwFriends.Models
{
    public class EventModel
    {
        [Key]
        public int Id { get; set; }
        public required string Title { get; set; }

        [Required]
        [DisplayName("host by")]
        public int Host { get; set; }

        [Required]
        [DisplayName("event time")]
        public DateTime DateTime { get; set; }

        [Required]
        public string Location { get; set; }
        public string? PictureId { get; set; }
        public string? Description { get; set; }
        [DisplayName("close form at")]
        public DateTime OpenUntil { get; set; }
        public List<UserModel> Attendees { get; set; }
        [Required]
        public List<string> tags { get; set; }
        public int spots { get; set; }
        public bool is_open { get; set; }
        //public int spots_left()
        //{ return spots - attendees.Count; }
    }
    public class EventViewModel
    {
        public List<EventModel> Eventsearch { get; set; }
        // Add other properties needed in the next view
    }
}