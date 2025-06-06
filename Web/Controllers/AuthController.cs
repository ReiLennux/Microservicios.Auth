using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Json;
using Web.Models;
using Web.Models.Auth;

namespace Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly MicroserviceSettings _settings;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly MsResponse _response;

        public AuthController(
            IOptions<MicroserviceSettings> settings,
            IHttpClientFactory httpClientFactory)
        {
            _settings = settings.Value;
            _httpClientFactory = httpClientFactory;
            _response = new MsResponse();
        }

        [HttpGet]
        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> Index(LoginRequestDto model)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.PostAsJsonAsync($"{_settings.LoginUrl}/login", model);

            if (response.IsSuccessStatusCode)
            { 

                var responseContent = await response.Content.ReadAsStringAsync();
                var deserializedResponse = JsonSerializer.Deserialize<MsResponse>(responseContent);

                if (deserializedResponse != null)
                {
                    HttpContext.Session.SetString("AuthToken", deserializedResponse.result?.ToString() ?? string.Empty);

                    var claims = new List<Claim>
                   {
                       new Claim(ClaimTypes.Name, model.UserName)
                   };

                    var identity = new ClaimsIdentity(claims, "Cookies");
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync("Cookies", principal);

                    return RedirectToAction("Index", "Home");
                }
            }

            ViewBag.Error = "Credenciales incorrectas";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookies");

            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Auth");
        }
    }
}
