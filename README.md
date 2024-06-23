This is simply a simple demo Tracking website for a delivery company backend fully built with asp.net core web api and using asp.net core mvc for accessing the api, 
where admin manage customers information and also update customers of thier progress of the delivery, whick customers can access thier information and delivery process from 
the website Home page with thier unique Track ID.

### MVC Controllers 

> HomeController
> ProfileController
> AdminController
> AdminTrackController

- [ ]  HomeController

 **Inde page:** 
`In the home page below image simply for customers to enter thier unique Tracking ID to access thier information and currect location or progress of the delivery 
`
> <img width="960" alt="Screenshot 2024-06-23 115346-indePge" src="https://github.com/Jay-code12/TrackingWebsite/assets/146625558/a2dd4f73-1592-40fd-992a-91e852f67153">

 **Show page:** 
`here all information of related to the the authenticated track ID are displayed
`
> <img width="960" alt="Screenshot 2024-06-23 115738-show" src="https://github.com/Jay-code12/TrackingWebsite/assets/146625558/9dfd84ae-bab9-4005-aa4f-68df0f21c176">

 **admin Login Page:** 
`simply a login page to access the admin dashboard
`

> <img width="957" alt="Screenshot 2024-06-23 120843-Login" src="https://github.com/Jay-code12/TrackingWebsite/assets/146625558/2338f426-4f93-4987-89d1-9b701ccbe928">


 **LogOut  Page:** 
`logout session is also included in the home controller
`

_Home Controller Source Code_

`namespace CourierMvcApiConsume.Controllers
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

    }
}
    `
