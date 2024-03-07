using Microsoft.AspNetCore.Mvc;
using FiwFriends.Models;
using Microsoft.AspNetCore.Hosting;

namespace FiwFriends.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        private string filePath;
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
    }
}
