using Microsoft.AspNetCore.Mvc;
using FiwFriends.Models;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System.Reflection;
using System.Diagnostics.Tracing;

namespace FiwFriends.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        private string filePath;
        private string filePath_event;
        private object userout;
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

        //public IActionResult Index()
        //{
            
        //    var usercookie = Request.Cookies["Username"];
        //    if (usercookie == null)
        //    {
        //        return RedirectToAction("Login","Home");
        //    }
        //    return getprofile(usercookie);
            
        //}


        public IActionResult Index(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                username = Request.Cookies["Username"]!;
                if (username == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                
            }
            return getprofile(username);

        }

        private IActionResult getprofile(string username) 
        {
            var jsonData = System.IO.File.ReadAllText(filePath);
            if (jsonData == null)
            {
                return RedirectToAction("Signup", "Home");
            }
            var userlist = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Profile>>(jsonData);
            if (userlist == null)
            {
                return RedirectToAction("Signup", "Home");
            }
            foreach (var user in userlist)
            {
                if (username == user.Username)
                {
                    return View(user);
                }
            }
            return RedirectToAction("Login", "Home");
        }
        
    }
}
