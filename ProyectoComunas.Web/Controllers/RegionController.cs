using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using ProyectoComunas.Web.Models;

namespace ProyectoComunas.Web.Controllers
{
    public class RegionController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RegionController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            var response = await client.GetAsync("/api/region");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var regiones = JsonSerializer.Deserialize<List<Region>>(json);
                return View(regiones);
            }

            return View(new List<Region>());
        }
    }
}
