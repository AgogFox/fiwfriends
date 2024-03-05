using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Test.Models;
using static Test.Models.Event;

namespace Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index() {
            return View(); 
        
        }

        /*public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Search(string word)
        {
            if (word == null) {
                return View();
            }
            var eventdb = GetEvents(file);
            var list = new List<EventOBJ>();
            foreach (var e in eventdb)
            {
                if (e.location.Contains(word) == true)
                {
                    list.Add(e);
                }
            }
            if (list.Count() == 0) {

                    ViewBag.errormsg = "Can not found";
                    return View();
            
            }
            return View(list);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }*/
    }
}
