using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.FeatureSliderDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.DefaultViewComponents
{
    public class CarouselDefaultComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory; // Backend API istekleri için HttpClientFactory kullanımı ve tanımlanması

        public CarouselDefaultComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
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
    }
}


