using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using Web.Models;
using Web.Models.Product;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly MicroserviceSettings _settings;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductsController(
            IOptions<MicroserviceSettings> setting,
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _settings = setting.Value;
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("AuthToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }

            var authToken = JsonSerializer.Deserialize<AuthResponse>(token);

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken.token);

            var response = await client.GetAsync($"{_settings.ProductUrl}/");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            var msResponse = JsonSerializer.Deserialize<MsResponse>(jsonString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var products = JsonSerializer.Deserialize<List<ProductDto>>(JsonSerializer.Serialize(msResponse.result), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return View(products);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("AuthToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }

            var authToken = JsonSerializer.Deserialize<AuthResponse>(token);
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken.token);

            var response = await client.DeleteAsync($"{_settings.ProductUrl}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            var msResponse = JsonSerializer.Deserialize<MsResponse>(jsonString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (msResponse != null && msResponse.isSuccess)
            {
                TempData["SuccessMessage"] = "Usuario registrado con éxito. Ahora puedes iniciar sesión.";
                return RedirectToAction("Index", "Users");
            }
            else
            {
                ViewBag.Error = msResponse.message;
            }

            return RedirectToAction("Index");


        }



    }
}
