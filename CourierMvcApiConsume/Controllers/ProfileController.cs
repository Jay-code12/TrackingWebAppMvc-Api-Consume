using CourierMvcApiConsume.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace CourierMvcApiConsume.Controllers
{

    public class ProfileController : ControllerBase
    {
        Uri baseAddress = new Uri("https://localhost:7264/api/");
        private readonly HttpClient _client;

        public ProfileController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

    }
}
