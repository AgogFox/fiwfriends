using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using FiwFriends.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;
using System.Xml;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;

namespace FiwFriends.Controllers
{
    public class HomeController : Controller
    {
        const string CookieUserId = "UserId";
        const string CookieUserName = "UserName";
        private readonly IWebHostEnvironment _webHostEnvironment;

        private string filePath;
        public HomeController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Data/UserDB.json");
        }

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
            var jsonData = System.IO.File.ReadAllText(filePath);
            var userlist = JsonSerializer.Deserialize<List<Usersystem>>(jsonData);
            foreach (var u in userlist) {
                if (u.Username == username) {
                    return true;
                }
            }
            return false;
        }

        public IActionResult Index()
        {
            var userdb = GetUsers(filePath);
            return View(userdb);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel{ RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
                    jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(userlist, Newtonsoft.Json.Formatting.Indented);
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
            var userdb = GetUsers(filePath);
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
                string modifiedJson = JsonConvert.SerializeObject(userdb, Newtonsoft.Json.Formatting.Indented);
                System.IO.File.WriteAllText(filePath, modifiedJson);
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
