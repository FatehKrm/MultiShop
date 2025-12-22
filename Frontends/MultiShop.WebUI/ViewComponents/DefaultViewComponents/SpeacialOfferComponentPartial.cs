using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.SpeacialOfferDtos;
using Newtonsoft.Json;
using System.Net.Http;

namespace MultiShop.WebUI.ViewComponents.DefaultViewComponents
{
    public class SpeacialOfferComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SpeacialOfferComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
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
    }
}
