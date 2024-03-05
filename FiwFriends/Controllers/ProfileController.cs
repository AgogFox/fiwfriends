using Microsoft.AspNetCore.Mvc;
using System.IO;
using Newtonsoft.Json;
using NuGet.DependencyResolver;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            var jsonData = System.IO.File.ReadAllText(@"C:\Users\Mon\Desktop\WebApplication1\WebApplication1\Database\UserDB.json");
            var userlist = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Profile>>(jsonData);
            return View(userlist);
            
        }
    }
}
