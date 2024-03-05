using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Newtonsoft.Json;

namespace WebApplication1.Controllers
{
    public class Editpage : Controller
    {
        private string json_path = "C:\\Users\\watsa\\OneDrive\\Desktop\\assignment\\Y2S2\\WebApplication1\\WebApplication1\\NewFolder\\data.json";
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult EditProfile()
        {
            return View();
        }
        [HttpPost]
        public async IActionResult EditProfile(Usersystem user)
        {
            if (user.Picture != null)
            {
                string folder = "C:\\Users\\watsa\\OneDrive\\Desktop\\assignment\\Y2S2\\WebApplication1\\WebApplication1\\Picture\\";
                folder += user.Picture.FileName + Guid.NewGuid().ToString();
                user.Picture_url = folder;
                string server_folder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                await user.Picture.CopyToAsync(new FileStream(server_folder, FileMode.Create)); ;
            }
            var jsonData = System.IO.File.ReadAllText(json_path);
            var user_list = JsonSerializer.Deserialize<List<Usersystem>>(jsonData);
            user_list.Add(user);
            jsonData = JsonConvert.SerializeObject(user_list, Formatting.Indented);
            System.IO.File.WriteAllText(json_path, jsonData);
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
