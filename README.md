This is simply a simple demo Tracking website for a delivery company backend fully built with asp.net core web api and using asp.net core mvc for accessing the api, 
where admin manage customers information and also update customers of thier progress of the delivery, whick customers can access thier information and delivery process from 
the website Home page with thier unique Track ID.

### MVC Controllers 

> HomeController
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

- [ ] AdminController


 **indePage:** 
`This page is only accessible by admin. Here new customers can be added, Customers records can be updated, Customer record can be Deleted also, you can also click on view customer at each roll to access track history of the specific customers view clicked
`
> <img width="958" alt="Screenshot 2024-06-23 123927-dmin2" src="https://github.com/Jay-code12/TrackingWebsite/assets/146625558/00ef266a-b476-4102-9db0-71e442cd089d">

 **AddTrack Page:** 
`Here admin can add new customers record to the database throught api
`

> <img width="941" alt="Screenshot 2024-06-23 123927-dmindd" src="https://github.com/Jay-code12/TrackingWebsite/assets/146625558/786ea90d-415c-4d51-8d77-f91d4d8d3870">

> <img width="958" alt="Screenshot 2024-06-23 124715-ddtrck" src="https://github.com/Jay-code12/TrackingWebsite/assets/146625558/8bb3cc20-b3af-4f8d-a02b-0b5fd75ce79c">

 **EditTrack Page:** 
`Here admin can Update customers record on the database throught api
`

> <img width="940" alt="Screenshot 2024-06-23 123927-dmin2edi" src="https://github.com/Jay-code12/TrackingWebsite/assets/146625558/d09ffe04-27aa-45d8-83cd-79793a8b024d">

> <img width="959" alt="Screenshot 2024-06-23 125731-edith" src="https://github.com/Jay-code12/TrackingWebsite/assets/146625558/cf376550-333d-4589-b44c-63ea4e4728a4">

 **Profile Page:** 
`Here admin can Update Login details to dashboard on the database throught api
`

> <img width="940" alt="Screenshot 2024-06-23 123927-profilelink" src="https://github.com/Jay-code12/TrackingWebsite/assets/146625558/e0c41c82-32a1-41f0-9ced-842b27a70102">

> <img width="959" alt="Screenshot 2024-06-23 151549-profile" src="https://github.com/Jay-code12/TrackingWebsite/assets/146625558/f5622430-fafc-4e9e-9424-d768a70a6a2d">


- [ ]  AdminTrackController

 **Inde page:** 
`Here current Location and progress of the customer goods is updated, admin can add new more history, update record or delete record if nessary
`

> <img width="940" alt="Screenshot 2024-06-23 123927-view" src="https://github.com/Jay-code12/TrackingWebsite/assets/146625558/c5ca3888-91e4-4360-bfbf-5e98c125f5e1">


> <img width="956" alt="Screenshot 2024-06-23 024951-ddHistory" src="https://github.com/Jay-code12/TrackingWebsite/assets/146625558/7f359355-75f5-4ac2-bd62-6c2c5ae6de2c">



