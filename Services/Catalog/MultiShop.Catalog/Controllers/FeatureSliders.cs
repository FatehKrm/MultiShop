using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.FeaturSliderDtos;
using MultiShop.Catalog.Services.FeaturSliderServices;

namespace MultiShop.Catalog.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureSliders : ControllerBase
    {
        private readonly IFeatureSliderService _featureSliderService;

        public FeatureSliders(IFeatureSliderService featureSliderService)
        {
            _featureSliderService = featureSliderService;
        }
        [HttpGet]
        public async Task<IActionResult> FeatureSliderList()
        {
            var values = await _featureSliderService.GetAllFeatureSliderAsync();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeatureSliderById(string id)
        {
            var values = await _featureSliderService.GetByIdFeatureSliderAsync(id);
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateFeatureSlider(CreateFeaturSliderDto createFeaturSliderDto)
        {
            await _featureSliderService.CreateFeatureSliderAsync(createFeaturSliderDto);
            return Ok("Veriler başarı ile eklendi");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteFeatureSlider(string id)
        {
            await _featureSliderService.DeleteFeatureSliderAsync(id);
            return Ok("Veriler başarı ile silindi");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateFeatureSlider(updateFeaturSliderDto updateFeaturSliderDto)
        {
            await _featureSliderService.UpdateFeatureSliderAsync(updateFeaturSliderDto);
            return Ok("Veriler başarı ile güncellendi");
        }
    }
}
