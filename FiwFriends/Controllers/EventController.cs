using Microsoft.AspNetCore.Mvc;
using FiwFriends.Models;

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

        private List<EventOBJ> GetEvents(string filepath)
        {
            if (System.IO.File.Exists(filepath))
            {
                var json = System.IO.File.ReadAllText(filepath);
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<EventOBJ>>(json);
            }
            else
            {
                Console.WriteLine("File does not exist.");
            }
            return null;
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
