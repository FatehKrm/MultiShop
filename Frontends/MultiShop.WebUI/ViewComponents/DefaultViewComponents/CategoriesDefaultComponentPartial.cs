using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.DefaultViewComponents
{
    public class CategoriesDefaultComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CategoriesDefaultComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task <IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7070/api/Categories");  // Yapacağım isteğin türünü belirleyecek ve adresini
            if (responseMessage.IsSuccessStatusCode)
            {
                var jasonData = await responseMessage.Content.ReadAsStringAsync(); // gelen veriyi string formatında okuyacak
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jasonData); // gelen string veriyi de deserialize edip CategoryDto tipine dönüştürecek
                return View(values);
            }
            return View();
        }
    }
}
