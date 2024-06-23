using CourierMvcApiConsume.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace CourierMvcApiConsume.Controllers
{
    public class AdminTrackController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7264/api/");
        private readonly HttpClient _client;

        public AdminTrackController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        public IActionResult Index(int id)
        {
            User getUserList = new User();
            HttpResponseMessage response2 = _client.GetAsync(_client.BaseAddress + "UserApi/GetUser/" + id).Result;

            if (response2.IsSuccessStatusCode)
            {

                string data = response2.Content.ReadAsStringAsync().Result;
                getUserList = JsonConvert.DeserializeObject<User>(data);

                ViewData["trackId"] = getUserList.TrackId;

            }


            List<TrackHistory> userList = new List<TrackHistory>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "TrackApi/GetTracks/" + id ).Result;

            if (response.IsSuccessStatusCode)
            {
                ViewData["userId"] = id;

                string data = response.Content.ReadAsStringAsync().Result;
                userList = JsonConvert.DeserializeObject<List<TrackHistory>>(data);
            }
            return View(userList);
        }

        public IActionResult AddTrack(int id)
        {
            User getUserList = new User();
            HttpResponseMessage response2 = _client.GetAsync(_client.BaseAddress + "UserApi/GetUser/" + id).Result;

            if (response2.IsSuccessStatusCode)
            {

                string data = response2.Content.ReadAsStringAsync().Result;
                getUserList = JsonConvert.DeserializeObject<User>(data);

                ViewData["trackId"] = getUserList.TrackId;

            }
            ViewData["userId"] = id;

            return View();
        }

        [HttpPost]
        public IActionResult AddTrack(TrackHistory track)
        {
            track.Id = 0; 

            string data = JsonConvert.SerializeObject(track);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "TrackApi/PostTrack", content).Result;

            if (response.IsSuccessStatusCode)
            {
                TempData["msg"] = "Successfully Added New Track Record To " + track.UserId;
                return RedirectToAction("Index", new { id = track.UserId });
            }

            return View();
        }

        public IActionResult Edit(int id)
        {
            User getUserList = new User();
            HttpResponseMessage response2 = _client.GetAsync(_client.BaseAddress + "UserApi/GetUser/" + id).Result;

            if (response2.IsSuccessStatusCode)
            {

                string data = response2.Content.ReadAsStringAsync().Result;
                getUserList = JsonConvert.DeserializeObject<User>(data);

                ViewData["trackId"] = getUserList.TrackId;

            }

            TrackHistory track = new TrackHistory();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "TrackApi/GetTrack/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                track = JsonConvert.DeserializeObject<TrackHistory>(data);
            }
            return View(track);
        }

        [HttpPost]
        public IActionResult Edit(TrackHistory track)
        {
            string data = JsonConvert.SerializeObject(track);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "TrackApi/EditTrack", content).Result;

            if (response.IsSuccessStatusCode)
            {
                var check = track.UserId;

                TempData["msg"] = "Successfully Edited Track Record";
                return RedirectToAction("Index", new {id = check});
            }

            return View();
        }

        public IActionResult Delete(int id)
        {
            TrackHistory userList = new TrackHistory();
            HttpResponseMessage response2 = _client.GetAsync(_client.BaseAddress + "TrackApi/GetTrack/" + id).Result;

            if (response2.IsSuccessStatusCode)
            {
                string data = response2.Content.ReadAsStringAsync().Result;
                userList = JsonConvert.DeserializeObject<TrackHistory>(data);
            }

            var TeId = userList.UserId;

            HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "TrackApi/DeleteTrack/" + id).Result;

            if (!response.IsSuccessStatusCode)
            {
                return NotFound("could not delete record");
            }

            TempData["msg"] = "Successfully Deleted Track Record from" + userList.UserId;
            return RedirectToAction("Index", new { id = TeId });
        }

    }
}
