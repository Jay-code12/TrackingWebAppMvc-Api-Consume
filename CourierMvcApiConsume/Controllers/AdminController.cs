using CourierMvcApiConsume.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace CourierMvcApiConsume.Controllers
{
    public class AdminController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7264/api/");
        private readonly HttpClient _client;

        public AdminController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }


        public IActionResult Index()
        {
            List<User> userList = new List<User>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "UserApi/GetUsers").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                userList = JsonConvert.DeserializeObject<List<User>>(data);
            }

            //var logged = JsonConvert.DeserializeObject<List<User>>(HttpContext.Session.GetString("Logged"));

            return View(userList);
        }


        public IActionResult AddTrack()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddTrack(User user)
        {
            string data = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "UserApi/PostUser", content).Result;

            if (response.IsSuccessStatusCode)
            {
                TempData["msg"] = "Successfully Added New Track Record";
                return RedirectToAction("Index");
            }

            return View();
        }


        public IActionResult Edit(int id)
        {
            User user = new User();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "UserApi/GetUser/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                user = JsonConvert.DeserializeObject<User>(data);
            }
            return View(user);
        }


        [HttpPost]
        public IActionResult Edit(User user)
        {
            string data = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "UserApi/EditUser", content).Result;

            if (response.IsSuccessStatusCode)
            {
                TempData["msg"] = "Successfully Edited Track Record";
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Delete(int id)
        {
            HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "UserApi/DeleteUser/" + id).Result;

            if (!response.IsSuccessStatusCode)
            {
                TempData["msg"] = "Failed Deleting Track Record";
                return RedirectToAction("Index");
            }
            TempData["msg"] = "Successfully Deleted Track Record";
            return RedirectToAction("Index");
        }

        public IActionResult Profile()
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
        public IActionResult Profile(AdminLogin user)
        {
            string data = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "Login/EditProfile", content).Result;

            if (response.IsSuccessStatusCode)
            {
                TempData["msgProfile"] = "Successfully Edited Profile Logins";
            }

            return View();
        }

    }
}
