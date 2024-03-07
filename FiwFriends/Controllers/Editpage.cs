using Microsoft.AspNetCore.Mvc;
using FiwFriends.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Newtonsoft.Json;
using System.Xml;

namespace FiwFriends.Controllers
{
    public class Editpage : Controller
    {
        private string json_path = "C:\\Users\\watsa\\OneDrive\\Desktop\\assignment\\Y2S2\\WebApplication1\\fiwfriends\\FiwFriends\\Data\\data.json";
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult EditProfile()
        {
            return View();
        }
        [HttpPost]
        public IActionResult EditProfile(Usersystem user)
        {
            string? username = "user1";
            var jsonData = System.IO.File.ReadAllText(json_path);
            List<Usersystem> user_list = JsonSerializer.Deserialize<List<Usersystem>>(jsonData);
            var current_user = user_list.FirstOrDefault(u => u.Username == username); ;
            if (current_user != null)
            {
                current_user.Username = user.Username;
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

            return RedirectToAction("Index");
        }
        public IActionResult DeleteProfile()
        {
            return View();
        }
        public IActionResult EditPassword()
        {
            return View();
        }
    }
}
