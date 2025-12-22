using MultiShop.Catalog.Dtos.CategoryDtos;
using MultiShop.Catalog.Dtos.FeaturSliderDtos;

namespace MultiShop.Catalog.Services.FeaturSliderServices
{
    public interface IFeatureSliderService
    {
        Task<List<ResultFeaturSliderDto>> GetAllFeatureSliderAsync();
        Task CreateFeatureSliderAsync(CreateFeaturSliderDto createFeaturSliderDto);
        Task UpdateFeatureSliderAsync(updateFeaturSliderDto updateFeaturSliderDto);
        Task DeleteFeatureSliderAsync(string id);
        Task<GetByIdFeatureSliderDto> GetByIdFeatureSliderAsync(string id);
        Task FeatureSliderChangeStatusToTure(string id);
        Task FeatureSliderChangeStatusToFalse(string id);
    }
}
