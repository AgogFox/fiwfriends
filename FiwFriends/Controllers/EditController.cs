using Microsoft.AspNetCore.Mvc;
using FiwFriends.Models;
using FiwFriends.Controllers;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Newtonsoft.Json;
using System.Xml;
using System.Reflection;

namespace FiwFriends.Controllers
{
    public class EditController : Controller
    {
        private string json_path = "C:\\Users\\watsa\\OneDrive\\Desktop\\assignment\\Y2S2\\WebApplication1\\fiwfriends\\FiwFriends\\wwwroot\\Data\\UserDB.json";
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Getuser()
        {
            int? userid = int.Parse(Request.Cookies["UserId"]);
            if (userid == null)
            {
                return RedirectToAction("Login");
            }
            var jsonData = System.IO.File.ReadAllText(json_path);
            List<Usersystem> user_list = JsonSerializer.Deserialize<List<Usersystem>>(jsonData);
            var current_user = user_list.FirstOrDefault(u => u.UserId == userid);
            if (current_user == null)
            {
                return Unauthorized("User not found.");
            }
            return Json(current_user);
        }
        public IActionResult EditProfile()
        {
            return View();
        }
        [HttpPost]
        public IActionResult EditProfile(Usersystem user)
        {
            string? username = Request.Cookies["UserName"];
            if (username == null)
            {
                return RedirectToAction("Login","Home");
            }
            var jsonData = System.IO.File.ReadAllText(json_path);
            List<Usersystem> user_list = JsonSerializer.Deserialize<List<Usersystem>>(jsonData);
            var current_user = user_list.FirstOrDefault(u => u.Username == username); ;
            if (current_user != null)
            {
                current_user.Name = user.Name;
                current_user.AboutMe = user.AboutMe;
                current_user.Location = user.Location;
                current_user.Interest = user.Interest;
                string modifiedJson = JsonConvert.SerializeObject(user_list, Newtonsoft.Json.Formatting.Indented);
                System.IO.File.WriteAllText(json_path, modifiedJson);
            }
            else
            {
                // Handle the case where the user is not found (optional)
                Console.WriteLine($"User '{username}' not found in the JSON file.");
            }

            return RedirectToAction("Index", "Home");
        }
        public IActionResult DeleteProfile()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DeleteProfile(Usersystem user)
        {
            int? userid = int.Parse(Request.Cookies["UserId"]);
            if (userid == null)
            {
                return RedirectToAction("Login");
            }
            var jsonData = System.IO.File.ReadAllText(json_path);
            List<Usersystem> user_list = JsonSerializer.Deserialize<List<Usersystem>>(jsonData);
            var current_user = user_list.FirstOrDefault(u => u.UserId == userid); ;
            if (current_user == null)
            {
                return Unauthorized("User not found.");
            }
            user_list.Remove(current_user);
            string modifiedJson = JsonConvert.SerializeObject(user_list, Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText(json_path, modifiedJson);
            Response.Cookies.Delete("UserId");
            Response.Cookies.Delete("UserName");
            return RedirectToAction("Index");
        }
        public IActionResult EditPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult EditPassword(OldNewpassword model)
        {
            int? userid = int.Parse(Request.Cookies["UserId"]);
            if (userid == null)
            {
                return RedirectToAction("Login");
            }
            var jsonData = System.IO.File.ReadAllText(json_path);
            List<Usersystem> user_list = JsonSerializer.Deserialize<List<Usersystem>>(jsonData);
            var current_user = user_list.FirstOrDefault(u => u.UserId == userid); ;
            if (current_user == null)
            {
                return Unauthorized("User not found.");
            }
            if (current_user.Password != model.OldPassword)
            {
                return BadRequest("Incorrect password");
            }
            current_user.Password = model.NewPassword;
            string modifiedJson = JsonConvert.SerializeObject(user_list, Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText(json_path, modifiedJson);


            return RedirectToAction("Index","Home");
        }
    }
}
