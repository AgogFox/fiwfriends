using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using WebAppproject.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Hosting.Server;
using Event.Models;



namespace WebAppproject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private string file = @"D:\Webapp\WebAppproject\Database\UserDB.json";
        const string CookieUserId = "UserId";
        const string CookieUserName = "UserName";

        private List<Usersystem> GetUsers(string filepath)
        {
            if (System.IO.File.Exists(filepath))
            {
                var json = System.IO.File.ReadAllText(filepath);
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Usersystem>>(json);
            }
            else
            {
                Console.WriteLine("File does not exist.");
            }
            return null;
        }

        private Boolean CheckDuplicateuser(string username) {
            var filePath = @"D:\Webapp\WebAppproject\Database\UserDB.json";
            var jsonData = System.IO.File.ReadAllText(filePath);
            var userlist = JsonSerializer.Deserialize<List<Usersystem>>(jsonData);
            foreach (var u in userlist) {
                if (u.Username == username) {
                    return true;
                }
            }
            return false;
        }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var userdb = GetUsers(file);
            return View(userdb);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //work here


        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Signup(Usersystem obj)
        {
            if (obj.Username != null && obj.Password != null)
            {
                var filePath = @"D:\Webapp\WebAppproject\Database\UserDB.json";
                var jsonData = System.IO.File.ReadAllText(filePath);
                var userlist = JsonSerializer.Deserialize<List<Usersystem>>(jsonData);
                if (CheckDuplicateuser(obj.Username) == true)
                {
                    ViewBag.duplicate = true;
                    return View();
                }
                else { 
                    obj.UserId = userlist.Count();
                    userlist.Add(obj);
                    jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(userlist, Formatting.Indented);
                    System.IO.File.WriteAllText(filePath, jsonData);
                    return RedirectToAction("Login");
                }
            }
            return View(obj);

        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string? username, string? password)
        {
            if (username == null || password == null) {
                ViewBag.error = "Please Enter Username and Password";
            }
            var userdb = GetUsers(file);
            Usersystem usertoModify = new Usersystem();
            foreach(var user in userdb) {
                if (user.Username == username && user.Password == password)
                {
                    usertoModify = user;
                    CookieOptions options = new CookieOptions();
                    options.Expires = DateTime.Now.AddDays(7);
                    Response.Cookies.Append(CookieUserId, user.UserId.ToString(), options);
                    Response.Cookies.Append(CookieUserName, user.Username.ToString(), options);
                }
                else {
                    usertoModify = null;
                }
            }
            if (usertoModify != null)
            {
                string modifiedJson = JsonConvert.SerializeObject(userdb, Formatting.Indented);
                System.IO.File.WriteAllText(file, modifiedJson);
                return RedirectToAction("Privacy");
            }
            else
            {
                Console.WriteLine($"User '{username}' not found in the JSON file.");
            }
            // Handle the case where the user is not found (optional)
            ViewBag.error = "404";
            return View();
        }
    }

}
