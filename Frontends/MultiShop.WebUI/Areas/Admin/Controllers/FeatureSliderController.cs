using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.FeatureSliderDtos;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/FeatureSlider")]
    public class FeatureSliderController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory; // Backend API istekleri için HttpClientFactory kullanımı ve tanımlanması

        public FeatureSliderController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            ViewBag.v1 = "Ana sayfa";
            ViewBag.v2 = "Slider";
            ViewBag.v3 = "Slider görseller listesi";
            ViewBag.v0 = "Slider görseller işlemleri";

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7070/api/FeatureSliders");  // Yapacağım isteğin türünü belirleyecek ve adresini
            if (responseMessage.IsSuccessStatusCode)
            {
                var jasonData = await responseMessage.Content.ReadAsStringAsync(); // gelen veriyi string formatında okuyacak
                var values = JsonConvert.DeserializeObject<List<ResultFeatureSliderDto>>(jasonData); // gelen string veriyi de deserialize edip FeatureSliderDto tipine dönüştürecek
                return View(values);
            }

            return View();
        }
        [HttpGet]
        [Route("CreateFeatureSlider")]
        public IActionResult CreateFeatureSlider()
        {
            ViewBag.v1 = "Ana sayfa";
            ViewBag.v2 = "Slider";
            ViewBag.v3 = "Slider görseller listesi";
            ViewBag.v0 = "Slider görseller işlemleri";
            return View();
        }
        [HttpPost]
        [Route("CreateFeatureSlider")]
        public async Task<IActionResult> CreateFeatureSlider(CreateFeatureSliderDto createFeatureSliderDto)
        {
            createFeatureSliderDto.Status = false;
            var client = _httpClientFactory.CreateClient();
            var JasonData = JsonConvert.SerializeObject(createFeatureSliderDto);
            StringContent stringContent = new StringContent(JasonData, Encoding.UTF8, "application/json");  // gönderilecek veriyi belirleyecek
            var responseMassage = await client.PostAsync("https://localhost:7070/api/FeatureSliders", stringContent);
            if (responseMassage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" });
            }
            return View();
        }
        [Route("DeleteFeatureSlider/{id}")]
        public async Task<IActionResult> DeleteFeatureSlider(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMassage = await client.DeleteAsync("https://localhost:7070/api/FeatureSliders?id=" + id);
            if (responseMassage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" });
            }
            return View();
        }
        [Route("UpdateFeatureSlider/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateFeatureSlider(string id)
        {
            ViewBag.v1 = "Ana sayfa";
            ViewBag.v2 = "Slider";
            ViewBag.v3 = "Slider görseller listesi";
            ViewBag.v0 = "Slider görseller işlemleri";

            var client = _httpClientFactory.CreateClient();
            var responseMassage = await client.GetAsync("https://localhost:7070/api/FeatureSliders/" + id);
            if (responseMassage.IsSuccessStatusCode)
            {
                var jsonData = await responseMassage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateFeatureSliderDto>(jsonData);
                return View(values);
            }
            return View();
        }
        [Route("UpdateFeatureSlider/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateFeatureSlider(UpdateFeatureSliderDto updateFeatureSliderDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateFeatureSliderDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMassage = await client.PutAsync("https://localhost:7070/api/FeatureSliders", stringContent);
            if (responseMassage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" });
            }
            return View();
        }
    }
}
