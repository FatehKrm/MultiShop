using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.FeaturSliderDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.FeaturSliderServices
{
    public class FeatureSliderService : IFeatureSliderService
    {
        private readonly IMongoCollection<FeatureSlider> _featureSliderCollection;
        private readonly IMapper _mapper;

        public FeatureSliderService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _featureSliderCollection = database.GetCollection<FeatureSlider>(_databaseSettings.FeatureSliderCollectionName);
            _mapper = mapper;
        }

        public async Task CreateFeatureSliderAsync(CreateFeaturSliderDto createFeaturSliderDto)
        {
            var values = _mapper.Map<FeatureSlider>(createFeaturSliderDto);
            await _featureSliderCollection.InsertOneAsync(values);
        }

        public async Task DeleteFeatureSliderAsync(string id)
        {
            await _featureSliderCollection.DeleteOneAsync(x => x.FeatureSliderId == id);
        }

        public Task FeatureSliderChangeStatusToFalse(string id)
        {
            throw new NotImplementedException();
        }

        public Task FeatureSliderChangeStatusToTure(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ResultFeaturSliderDto>> GetAllFeatureSliderAsync()
        {
            var values = await _featureSliderCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultFeaturSliderDto>>(values);
        }

        public async Task<GetByIdFeatureSliderDto> GetByIdFeatureSliderAsync(string id)
        {
            var values = await _featureSliderCollection.Find(x => x.FeatureSliderId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdFeatureSliderDto>(values);    
        }

        public async Task UpdateFeatureSliderAsync(updateFeaturSliderDto updateFeaturSliderDto)
        {
            var values = _mapper.Map<FeatureSlider>(updateFeaturSliderDto);
            await _featureSliderCollection.FindOneAndReplaceAsync(x => x.FeatureSliderId == updateFeaturSliderDto.FeatureSliderId, values);
        }
    }
}
