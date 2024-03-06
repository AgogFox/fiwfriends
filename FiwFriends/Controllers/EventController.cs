using Event.Models;
using Microsoft.AspNetCore.Mvc;
using FiwFriends.Models;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace FiwFriends.Controllers
{
    public class EventController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        private string filePath;
        public EventController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Data/Event.json");
        }


        public IActionResult Index()
        {
            return View();

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

        public IActionResult Search()
        {
            return View();
        }
        
            
        [HttpPost]
        public IActionResult Search(string word)
        {
            if (word == null)
            {
                return View();
            }
            var eventdb = GetEvents(filePath);
            var list = new List<EventOBJ>();
            foreach (var e in eventdb)
            {
                if (e.location.Contains(word) == true)
                {
                    list.Add(e);
                }
            }
            if (list.Count() == 0)
            {

                ViewBag.errormsg = "Can not found";
                return View();
            }
            ViewBag.word = word;
            return View(list);
        }
    }
}
