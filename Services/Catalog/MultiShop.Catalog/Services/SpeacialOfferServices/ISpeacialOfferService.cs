using MultiShop.Catalog.Dtos.SpeacialOfferDtos;

namespace MultiShop.Catalog.Services.SpeacialOfferServices
{
    public interface ISpeacialOfferService
    {
        Task<List<ResultSpeacialOfferDto>> GetAllSpeacialOfferAsync();
        Task CreateSpeacialOfferAsync(CreateSpeacialOfferDto createSpeacialOfferDto);
        Task UpdateSpeacialOfferAsync(UpdateSpeacialOfferDto updateSpeacialOfferDto);
        Task DeleteSpeacialOfferAsync(string id);
        Task<GetByIdSpeacialOfferDto> GetByIdSpeacialOfferAsync(string id);
    }
}
