using Microsoft.AspNetCore.Mvc;
using FiwFriends.Models;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace FiwFriends.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        private string filePath;
        private string filePath_event;
        private List<EventOBJ> GetEvents(string filepath_event)
        {
            if (System.IO.File.Exists(filepath_event))
            {
                var json = System.IO.File.ReadAllText(filepath_event);

                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<EventOBJ>>(json);
            }
            else
            {
                Console.WriteLine("File does not exist.");
                return null;
            }
        }
        public ProfileController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Data/UserDB.json");
        }

        public IActionResult Index()
        {
            var jsonData = System.IO.File.ReadAllText(filePath);
            var userlist = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Profile>>(jsonData);
            return View(userlist);

        }

        public IActionResult juux()
        {
            var events = GetEvents(filePath_event);
            return View(events);
        }
    }
}
