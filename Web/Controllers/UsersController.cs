using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Runtime;
using Web.Models;


namespace Web.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly MicroserviceSettings _settings;
        private readonly IHttpClientFactory _httpClientFactory;

        public UsersController(
            IOptions<MicroserviceSettings> settings,
            IHttpClientFactory httpClientFactory)
        {
            _settings = settings.Value;
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index() => View();



        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> CreateAsync(RegisterRequestDto model)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.PostAsJsonAsync($"{_settings.LoginUrl}/register", model);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<MsResponse>();

                if (content != null && content.IsSuccess)
                {
                    TempData["SuccessMessage"] = "Usuario registrado con éxito. Ahora puedes iniciar sesión.";
                    return RedirectToAction("Index", "Users");
                }else
                {
                    ViewBag.Error = content.Message;
                }
            }else
            {
                ViewBag.Error = "Ocurrió un error al registrar el usuario.";
            }
            return View();
        }
    }
}
