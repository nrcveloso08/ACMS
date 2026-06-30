using ATS.Data.Repository;
using ATS.DTO.AdvertisementSource;
using ATS.DTO.Document;
using ATS.DTO.Geo;
using ATS.DTO.InterviewSchedule;
using ATS.DTO.Location;

namespace ATS.Service
{
    public interface ILookupsService
    {
        Task<List<Location>> GetLocationsAsync();
        Task<Dictionary<int, string>> GetLanguagesAsync();
        Task<IList<CountryDTO>> GetCountriesAsync();
        Task<IList<StateDTO>> GetStatesAsync(int countryId);
        Task<IList<CityDTO>> GetCitiesAsync(int stateId);
        Task<List<AdvertisementSource>> GetAdvertisementSourcesAsync();
        Task<IList<DocumentType>> GetDocumentTypesAsync();
        Task<IList<InterviewStatusLookup>> GetInterviewStatusesAsync();
    }

    public class LookupsService : ILookupsService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly ILanguageRepository _languageRepository;
        private readonly IGeoRepository _geoRepository;
        private readonly IAdvertisementSourceRepository _advertisementSourceRepository;
        private readonly IDocumentTypeRepository _documentTypeRepository;
        private readonly IInterviewStatusRepository _interviewStatusRepository;

        public LookupsService(
            ILocationRepository locationRepository,
            ILanguageRepository languageRepository,
            IGeoRepository geoRepository,
            IAdvertisementSourceRepository advertisementSourceRepository,
            IDocumentTypeRepository documentTypeRepository,
            IInterviewStatusRepository interviewStatusRepository)
        {
            _locationRepository = locationRepository;
            _languageRepository = languageRepository;
            _geoRepository = geoRepository;
            _advertisementSourceRepository = advertisementSourceRepository;
            _documentTypeRepository = documentTypeRepository;
            _interviewStatusRepository = interviewStatusRepository;
        }

        public async Task<List<Location>> GetLocationsAsync() => await _locationRepository.GetAll();

        public async Task<Dictionary<int, string>> GetLanguagesAsync() => await _languageRepository.GetAll();

        public async Task<IList<CountryDTO>> GetCountriesAsync() => await _geoRepository.GetCountries();

        public async Task<IList<StateDTO>> GetStatesAsync(int countryId) => await _geoRepository.GetStates(countryId);

        public async Task<IList<CityDTO>> GetCitiesAsync(int stateId) => await _geoRepository.GetCities(stateId);

        public async Task<List<AdvertisementSource>> GetAdvertisementSourcesAsync() => await _advertisementSourceRepository.GetAll();

        public async Task<IList<DocumentType>> GetDocumentTypesAsync() => await _documentTypeRepository.GetDocumentTypes();

        public async Task<IList<InterviewStatusLookup>> GetInterviewStatusesAsync() => await _interviewStatusRepository.GetAllInterviewStatuses();
    }
}
