using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Domain.Entities;
using System.Text;

namespace UserInterface.Controllers
{
    public class ConfigurationItemsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ConfigurationItemsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:44308/api/Configuration/all");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ConfigurationItem>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateConfiguration()
        {
            var model = new ConfigurationItem();
            model.Id = Guid.NewGuid().ToString();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateConfiguration(ConfigurationItem configurationModel)
        {
            
            if (ModelState.IsValid)
            {
                var jsonData = JsonConvert.SerializeObject(configurationModel);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var client = _httpClientFactory.CreateClient();
                var responseMessage = await client.PostAsync("https://localhost:44308/api/Configuration", content);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateConfiguration([FromRoute] string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:44308/api/Configuration/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<ConfigurationItem>(jsonData);
                return View(value);
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> UpdateConfiguration(ConfigurationItem configurationModel)
        {
            var jsonData = JsonConvert.SerializeObject(configurationModel);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.PutAsync("https://localhost:44308/api/Configuration", content);

            if (responseMessage.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View();
        }


        [HttpGet]
        public async Task<IActionResult> DeleteConfiguration([FromRoute] string id)
        {
       
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:44308/api/Configuration/{id}");

            if (responseMessage.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View();
        }

    }
}

