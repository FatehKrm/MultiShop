using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.SpeacialOfferDtos;
using MultiShop.Catalog.Services.SpeacialOfferServices;

namespace MultiShop.Catalog.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class SpeacialOffersController : ControllerBase
    {
        private readonly ISpeacialOfferService speacialOfferService;

        public SpeacialOffersController(ISpeacialOfferService speacialOfferService)
        {
            this.speacialOfferService = speacialOfferService;
        }
        [HttpGet]
        public async Task<IActionResult> SpeacialOfferList()
        {
            var values = await speacialOfferService.GetAllSpeacialOfferAsync();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSpeacialOfferById(string id)
        {
            var values = await speacialOfferService.GetByIdSpeacialOfferAsync(id);
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateSpeacialOffer(CreateSpeacialOfferDto createSpeacialOfferDto)
        {
            await speacialOfferService.CreateSpeacialOfferAsync(createSpeacialOfferDto);
            return Ok("Özel teklifler başarıyla eklendi");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteSpeacialOffer(string id)
        {
            await speacialOfferService.DeleteSpeacialOfferAsync(id);
            return Ok("Özel teklifler başarıyla silindi");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateSpeacialOffer(UpdateSpeacialOfferDto updateSpeacialOfferDto)
        {
            await speacialOfferService.UpdateSpeacialOfferAsync(updateSpeacialOfferDto);
            return Ok("Özel teklifler başarıyla güncellendi");

        }
    }
}
