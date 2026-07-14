// C# Frontends\MultiShop.WebUI\ViewComponents\ProductDetailViewComponents\ProductDetailImageSliderCopmponentPartial.cs
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ProductImageDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.ProductDetailViewComponents
{
    public class ProductDetailImageSliderCopmponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductDetailImageSliderCopmponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            var client = _httpClientFactory.CreateClient();

            // Guard: if id is null or empty, return an empty model immediately
            if (string.IsNullOrWhiteSpace(id))
            {
                return View(new GetByIdProductImageDto());
            }

            // Build query correctly and encode id
            var url = $"https://localhost:7070/api/ProductImages/ProductImagesByProductId?id={id}";
            var responseMessage = await client.GetAsync(url);

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<GetByIdProductImageDto>(jsonData);
                return View(values ?? new GetByIdProductImageDto());
            }

            // Always return a non-null model to avoid Razor NREs
            return View(new GetByIdProductImageDto());
        }
    }
}