using Microsoft.AspNetCore.Mvc;
using FiwFriends.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Newtonsoft.Json;


namespace FiwFriends.Controllers
{
    public class EditController : Controller
    {
        private string json_path;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public EditController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            json_path = Path.Combine(_webHostEnvironment.WebRootPath, "Data/UserDB.json");
        }
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
            current_user.Picture = null;
            return Json(current_user);
        }
        public IActionResult EditProfile()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(Usersystem user)
        {
            string? username = Request.Cookies["UserName"];
            if (user.Picture != null)
            {
                string folder = "Picture/user-picture/";
                folder += Guid.NewGuid().ToString() + user.Picture.FileName;
                user.Picture_url = "/" + folder;

                string server_folder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                await user.Picture.CopyToAsync(new FileStream(server_folder, FileMode.Create)); ;
            }
            if (username == null)
            {
                return RedirectToAction("Login","Home");
            }
            var jsonData = System.IO.File.ReadAllText(json_path);
            List<Usersystem> user_list = JsonSerializer.Deserialize<List<Usersystem>>(jsonData);
            var current_user = user_list.FirstOrDefault(u => u.Username == username);
            current_user.Picture = null;
            if (current_user != null)
            {
                current_user.Name = user.Name;
                current_user.AboutMe = user.AboutMe;
                current_user.Location = user.Location;
                current_user.Interest = user.Interest;
                current_user.Picture_url = user.Picture_url;
                string modifiedJson = JsonConvert.SerializeObject(user_list, Newtonsoft.Json.Formatting.Indented);
                System.IO.File.WriteAllText(json_path, modifiedJson);
            }

            return RedirectToAction("Index", "Home");
        }
        public IActionResult DeleteProfile()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DeleteProfile(int attendees)
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
            var current_user = user_list.FirstOrDefault(u => u.UserId == userid);
            if (current_user == null)
            {
                return Unauthorized("User not found.");
            }
            if (current_user.Password != model.OldPassword)
            {
                return ViewBag("Incorrect password");
            }
            foreach(var user in user_list)
            {
                if(user.UserId == current_user.UserId)
                {
                    user.Password = model.NewPassword;
                    break;
                }
            }
            string modifiedJson = JsonConvert.SerializeObject(user_list, Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText(json_path, modifiedJson);


            return RedirectToAction("Index","Home");
            return View();
        }
        
    }
}
