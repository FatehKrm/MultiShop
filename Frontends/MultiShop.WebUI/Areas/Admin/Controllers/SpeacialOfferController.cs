using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.SpeacialOfferDtos;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    [Route("Admin/SpeacialOffer")]
    public class SpeacialOfferController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SpeacialOfferController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            ViewBag.v1 = "Ana sayfa";
            ViewBag.v2 = "Özel teklifler";
            ViewBag.v3 = "özel teklif listesi";
            ViewBag.v0 = "özel teklif işlemleri";

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7070/api/SpeacialOffers");  // Yapacağım isteğin türünü belirleyecek ve adresini
            if (responseMessage.IsSuccessStatusCode)
            {
                var jasonData = await responseMessage.Content.ReadAsStringAsync(); // gelen veriyi string formatında okuyacak
                var values = JsonConvert.DeserializeObject<List<ResultSpeacialOfferDto>>(jasonData); // gelen string veriyi de deserialize edip SpeacialOfferDto tipine dönüştürecek
                return View(values);
            }

            return View();
        }
        [HttpGet]
        [Route("CreateSpeacialOffer")]
        public async Task<IActionResult> CreateSpeacialOffer()
        {
            ViewBag.v1 = "Ana sayfa";
            ViewBag.v2 = "Özel teklifler";
            ViewBag.v3 = "özel teklif listesi";
            ViewBag.v0 = "özel teklif işlemleri";
            return View();
        }
        [HttpPost]
        [Route("CreateSpeacialOffer")]
        public async Task<IActionResult> CreateSpeacialOffer(CreateSpeacialOfferDto createSpeacialOfferDto)
        {
            var client = _httpClientFactory.CreateClient();
            var JasonData = JsonConvert.SerializeObject(createSpeacialOfferDto);
            StringContent stringContent = new StringContent(JasonData, Encoding.UTF8, "application/json");  // gönderilecek veriyi belirleyecek
            var responseMassage = await client.PostAsync("https://localhost:7070/api/SpeacialOffers", stringContent);
            if (responseMassage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "SpeacialOffer", new { area = "Admin" });
            }
            return View();
        }
        [Route("DeleteSpeacialOffer/{id}")]
        public async Task<IActionResult> DeleteSpeacialOffer(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMassage = await client.DeleteAsync("https://localhost:7070/api/SpeacialOffers?id=" + id);
            if (responseMassage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "SpeacialOffer", new { area = "Admin" });
            }
            return View();
        }
        [Route("UpdateSpeacialOffer/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateSpeacialOffer(string id)
        {
            ViewBag.v1 = "Ana sayfa";
            ViewBag.v2 = "Özel teklifler";
            ViewBag.v3 = "özel teklif listesi";
            ViewBag.v0 = "özel teklif işlemleri";

            var client = _httpClientFactory.CreateClient();
            var responseMassage = await client.GetAsync("https://localhost:7070/api/SpeacialOffers/" + id);
            if (responseMassage.IsSuccessStatusCode)
            {
                var jsonData = await responseMassage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateSpeacialOfferDto>(jsonData);
                return View(values);
            }
            return View();
        }
        [Route("UpdateSpeacialOffer/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateSpeacialOffer(UpdateSpeacialOfferDto updateSpeacialOfferDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateSpeacialOfferDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMassage = await client.PutAsync("https://localhost:7070/api/SpeacialOffers", stringContent);
            if (responseMassage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "SpeacialOffer", new { area = "Admin" });
            }
            return View();
        }
    }
}
