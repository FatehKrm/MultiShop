using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.SpeacialOfferDtos;
using MultiShop.Catalog.Dtos.SpeacialOfferDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.SpeacialOfferServices
{
    public class SpeacialOfferService : ISpeacialOfferService
    {
        private readonly IMongoCollection<SpeacialOffer> _speacialOfferCollection;
        private readonly IMapper _mapper;

        public SpeacialOfferService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _speacialOfferCollection = database.GetCollection<SpeacialOffer>(_databaseSettings.SpeacialOfferCollectionName);
            _mapper = mapper;
        }
        public async Task CreateSpeacialOfferAsync(CreateSpeacialOfferDto createSpeacialOfferDto)
        {
            var value = _mapper
                .Map<SpeacialOffer>(createSpeacialOfferDto);
            await _speacialOfferCollection.InsertOneAsync(value);
        }

        public async Task DeleteSpeacialOfferAsync(string id)
        {
            await _speacialOfferCollection
                .DeleteOneAsync(x => x.SpeacialOfferId == id);
        }

        public async Task<List<ResultSpeacialOfferDto>> GetAllSpeacialOfferAsync()
        {
            var values = await _speacialOfferCollection
                 .Find(x => true)
                 .ToListAsync();
            return _mapper.Map<List<ResultSpeacialOfferDto>>(values);
        }

        public async Task<GetByIdSpeacialOfferDto> GetByIdSpeacialOfferAsync(string id)
        {
            var values = await _speacialOfferCollection  // SpeacialOffer Collection sql server da tablo gibi düşünülebilir
               .Find<SpeacialOffer>(x => x.SpeacialOfferId == id)
               .FirstOrDefaultAsync();
            return _mapper.Map<GetByIdSpeacialOfferDto>(values);
        }

        public async Task UpdateSpeacialOfferAsync(UpdateSpeacialOfferDto updateSpeacialOfferDto)
        {
            var values = _mapper.Map<SpeacialOffer>(updateSpeacialOfferDto);
            await _speacialOfferCollection
                .FindOneAndReplaceAsync(x => x.SpeacialOfferId == updateSpeacialOfferDto.SpeacialOfferId, values);
        }
    }
}
