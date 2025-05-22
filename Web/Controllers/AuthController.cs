using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Web.Models;

namespace Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly MicroserviceSettings _settings;
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthController(
            IOptions<MicroserviceSettings> settings,
            IHttpClientFactory httpClientFactory)
        {
            _settings = settings.Value;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> Index(LoginRequestDto model)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.PostAsJsonAsync($"{_settings.LoginUrl}/login", model);

            Console.WriteLine(response);
            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                HttpContext.Session.SetString("AuthToken", token);
                return RedirectToAction("Welcome");
            }

            ViewBag.Error = "Credenciales incorrectas";
            return View();
        }

        public IActionResult Welcome()
        {
            var token = HttpContext.Session.GetString("AuthToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Index");
            }

            ViewBag.Token = token;
            return View();
        }
    }
}
