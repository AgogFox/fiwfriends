using Microsoft.AspNetCore.Mvc;

namespace FiwFriends.Controllers
{
    public class EventController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
