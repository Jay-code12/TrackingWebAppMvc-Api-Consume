using Azure.Identity;
using CourierMvcApiConsume.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace CourierMvcApiConsume.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        Uri baseAddress = new Uri("https://localhost:7264/api/");
        private readonly HttpClient _client;

        public HomeController(ILogger<HomeController> logger)
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();

        }

        [HttpPost]
        public IActionResult Index(User user)
        {
            return RedirectToAction("Show", "Home", new { track = user.TrackId });
            //return View();

        }

        public IActionResult Show(string track) 
        {
            User user = new User();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "ViewTrack/GetUser/" + track).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                user = JsonConvert.DeserializeObject<User>(data);
            }

            TempData["sName"] = user.SenderName;
            TempData["sContact"] = user.SenderContact;

            TempData["rName"] = user.ReceiverName;
            TempData["rContact"] = user.ReceiverContact;
            TempData["rAddress"] = user.ReceiverAddress;

            TempData["cret"] = user.Created;



            List<TrackHistory> viewTrck = new List<TrackHistory>();
            HttpResponseMessage response2 = _client.GetAsync(_client.BaseAddress + "ViewTrack/ViewTracks/" + user.Id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data2 = response2.Content.ReadAsStringAsync().Result;
                viewTrck = JsonConvert.DeserializeObject<List<TrackHistory>>(data2);
            }

            return View(viewTrck);
        }

        public IActionResult Login()
        {
            int id = 1;

            AdminLogin user = new AdminLogin();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "Login/Profile/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                user = JsonConvert.DeserializeObject<AdminLogin>(data);
            }
            return View(user);
        }

        [HttpPost]
        public IActionResult Login(AdminLogin track)
        {
            string data = JsonConvert.SerializeObject(track);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "Login/Login", content).Result;

            if (response.IsSuccessStatusCode)
            {
                var check = track.UserName;

                List<Claim> lst = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, track.UserName),
                new Claim(ClaimTypes.Name, track.UserName)

            };
                ClaimsIdentity ci = new ClaimsIdentity(lst,
                    Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal cp = new ClaimsPrincipal(ci);

                HttpContext.SignInAsync(cp);

                return RedirectToAction("Index", "Admin");
            }
            //HttpContext.Session.SetString("Logged", data);


            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Login", "home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
