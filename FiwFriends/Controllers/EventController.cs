﻿using FiwFriends.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace FiwFriends.Controllers
{
    public class EventController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        private string filePath;
        private string filePath_user;
        public EventController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Data/Event.json");
            filePath_user = Path.Combine(_webHostEnvironment.WebRootPath, "Data/UserDB.json");
        }

        private List<EventOBJ> GetEvents(string filepath)
        {
            if (System.IO.File.Exists(filepath))
            {
                var json = System.IO.File.ReadAllText(filepath);
                return JsonConvert.DeserializeObject<List<EventOBJ>>(json);
            }
            else
            {
                Console.WriteLine("File does not exist.");
            }
            return null;
        }

        private List<Usersystem> GetUser(string filepath)
        {
            if (System.IO.File.Exists(filepath))
            {
                var json = System.IO.File.ReadAllText(filepath);
                return JsonConvert.DeserializeObject<List<Usersystem>>(json);
            }
            else
            {
                Console.WriteLine("File does not exist.");
            }
            return null;
        }

        public IActionResult Index()
        {
            return View();

        }

        public IActionResult AddEvent()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddEventAsync(EventOBJ obj)
        {
            string? id = Request.Cookies["UserId"];
            string? username = Request.Cookies["UserName"];
            if (obj.picture != null)
            {
                string folder = "event_pic/picture/";
                folder += Guid.NewGuid().ToString() + obj.picture.FileName; 
                obj.picture_url = "/" + folder;

                string server_folder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                await obj.picture.CopyToAsync(new FileStream(server_folder, FileMode.Create)); ;
            }
            var jsonData = System.IO.File.ReadAllText(filePath);
            var event_list = JsonSerializer.Deserialize<List<EventOBJ>>(jsonData);
            obj.host_by = username;
            obj.picture = null;
            if (obj.attendees == null)
            {
                obj.attendees = [];
            }
            event_list.Add(obj);
            jsonData = JsonConvert.SerializeObject(event_list, Formatting.Indented); 
            System.IO.File.WriteAllText(filePath, jsonData);
            return RedirectToAction("Search");
        }

        public IActionResult ShowEvent(string? title)
        {
            var jsonData = System.IO.File.ReadAllText(filePath);
            var event_list = JsonSerializer.Deserialize<List<EventOBJ>>(jsonData);
            EventOBJ obj = new EventOBJ();
            foreach (EventOBJ e in event_list)
            {
                if (e.title == title)
                {
                    obj = e;
                    break;
                }
            }
            return View(obj);
        }

        public IActionResult Search(string? word)
        {
            if (word == null)
            {
                return View();
            }
            var eventdb = GetEvents(filePath);
            var list = new List<EventOBJ>();
            foreach (var e in eventdb)
            {
                if (e.location.Contains(word) == true)
                {
                    list.Add(e);
                }
            }
            if (list.Count() == 0)
            {

                ViewBag.errormsg = "Can not found";
                return View();
            }
            ViewBag.word = word;
            return View(list);
        }

        public IActionResult Attend(string title)
        {
            string? id = Request.Cookies["UserId"];

            List<Usersystem> user_list = GetUser(filePath_user);
            List<EventOBJ> event_list = GetEvents(filePath);
            int i = 0;
            int j = 0;
            foreach(Usersystem u in user_list)
            {
                if (id == u.UserId.ToString())
                {
                    if (u.Event == null)
                    {
                        user_list[i].Event = [];
                    }
                    
                    foreach (EventOBJ e in event_list)
                    {
                        if (e.title == title)
                        {
                            event_list[j].attendees.Add(user_list[i].UserId);
                            user_list[i].Event.Add(event_list[j]);
                            break;
                        }
                        j++;
                    }
                    break;
                }
                i++;
            }
            var jsonData = JsonConvert.SerializeObject(event_list, Formatting.Indented);
            System.IO.File.WriteAllText(filePath, jsonData);
            jsonData = JsonConvert.SerializeObject(user_list, Formatting.Indented);
            System.IO.File.WriteAllText(filePath_user, jsonData);
            return RedirectToAction("ShowEvent", new {title = title});
        }
    }
}
