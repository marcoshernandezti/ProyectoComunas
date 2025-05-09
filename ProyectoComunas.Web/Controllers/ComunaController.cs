using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using ProyectoComunas.Web.Models;

namespace ProyectoComunas.Web.Controllers
{
    public class ComunaController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ComunaController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        //public async Task<IActionResult> Index()
        //{
        //    var client = _httpClientFactory.CreateClient("ApiClient");
        //    var response = await client.GetAsync("/api/comuna");
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var json = await response.Content.ReadAsStringAsync();
        //        var comunas = JsonSerializer.Deserialize<List<Comuna>>(json);
        //        return View(comunas);
        //    }

        //    return View(new List<Comuna>());
        //}

        public async Task<IActionResult> Index(int idRegion)
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            var response = await client.GetAsync($"/api/region/{idRegion}/comuna");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var comunas = JsonSerializer.Deserialize<List<Comuna>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return View(comunas);
            }

            return View(new List<Comuna>());
        }
    }
}
