using Event.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Event.Controllers
{
    public class EventController : Controller
    {
        private string filePath = @"D:\test_webapp\Event\Event\Data\Event.json";
        private readonly IWebHostEnvironment _webHostEnvironment;
        public EventController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var jsonData = System.IO.File.ReadAllText(filePath);
            var event_list = JsonConvert.DeserializeObject<List<EventOBJ>>(jsonData);

            return View(event_list);
        }
        public IActionResult AddEvent()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddEventAsync(EventOBJ obj)
        {

                if (obj.picture != null)
                {
                    string folder = "event_pic/picture/";
                    folder += obj.picture.FileName;
                    obj.picture_url = "/" + folder;

                    string server_folder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                    await obj.picture.CopyToAsync(new FileStream(server_folder, FileMode.Create)); ;
                }
                var jsonData = System.IO.File.ReadAllText(filePath);
                var event_list = JsonSerializer.Deserialize<List<EventOBJ>>(jsonData);
                obj.picture = null;
                if (obj.attendees == null)
                {
                    obj.attendees = [];
                }
                event_list.Add(obj);
                jsonData = JsonConvert.SerializeObject(event_list, Formatting.Indented); 
                System.IO.File.WriteAllText(filePath, jsonData);
                return RedirectToAction("Index");
        }
        
        public IActionResult ShowEvent(string? title)
        {
            var jsonData = System.IO.File.ReadAllText(filePath);
            var event_list = JsonSerializer.Deserialize<List<EventOBJ>>(jsonData);
            EventOBJ obj = new EventOBJ();
            foreach(EventOBJ e in event_list)
            {
                if (e.title == title)
                {
                    obj = e;
                    break;
                }
            }
            return View(obj);
        }
        public IActionResult Summary(EventOBJ obj)
        {
            return View(obj);
        }
    }
}
