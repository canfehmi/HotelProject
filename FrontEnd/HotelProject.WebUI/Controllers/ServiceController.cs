using HotelProject.WebUI.Dtos.ServiceDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace HotelProject.WebUI.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ServiceController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task< IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMesaj = await client.GetAsync("http://localhost:5129/api/Service");
            if(responseMesaj.IsSuccessStatusCode)
            {
                var jsonData = await responseMesaj.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultServiceDto>>(jsonData);
                return View(values);
            }
            return View();
        }
        [HttpGet]
        public IActionResult AddService()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddService(CreateServiceDto createServiceDto)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            var client = _httpClientFactory.CreateClient();
           var jsonData=JsonConvert.SerializeObject(createServiceDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMesaj = await client.PostAsync("http://localhost:5129/api/Service", stringContent);
            if(responseMesaj.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> DeleteService(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMesaj = await client.DeleteAsync($"http://localhost:5129/api/Service?id={id}");
            if(responseMesaj.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public async Task< IActionResult> UpdateService(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMesaj = await client.GetAsync($"http://localhost:5129/api/Service/{id}");
            if(responseMesaj.IsSuccessStatusCode)
            {
                var jsonData = await responseMesaj.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateServiceDto>(jsonData);
                return View(value);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateService(UpdateServiceDto dto)
        {
            if(!ModelState.IsValid)
            {
                return View(dto);
            }
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMesaj = await client.PutAsync("http://localhost:5129/api/Service/", content);
            if(responseMesaj.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
