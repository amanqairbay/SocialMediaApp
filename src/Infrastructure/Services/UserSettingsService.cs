using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using Core.Services;

namespace Infrastructure.Services
{
    /// <summary>
    /// Represents the user settings class.
    /// </summary>
    public class UserSettingsService : IUserSettingsService
    {
        #region Fields
        private readonly IRepository<Region> _regionRepository;
        private readonly IRepository<City> _cityRepository;
        private readonly IRepository<Gender> _genderRepository;
        private readonly IRepository<Status> _statusRepository;
        private readonly IMapper _mapper;
        #endregion Fields

        #region Constructor
        public UserSettingsService(
            IRepository<Region> regionRepository,
            IRepository<City> cityRepository,
            IRepository<Gender> genderRepository,
            IRepository<Status> statusRepository,
            IMapper mapper)
        {
            _regionRepository = regionRepository;
            _cityRepository = cityRepository;
            _genderRepository = genderRepository;
            _statusRepository = statusRepository;
            _mapper = mapper;
        }
        #endregion Constructor

        #region Get methods
        /// <summary>
        /// Gets and returns a list of regions.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the list of regions.
        /// </returns>
        public async Task<IEnumerable<RegionDto>> GetRegionsAsync()
        {
            var regionsFromDb = await _regionRepository.GetAllAsync();
            var regionsDto = _mapper.Map<IEnumerable<Region>, IEnumerable<RegionDto>>(regionsFromDb);

            return regionsDto;
        }

        /// <summary>
        /// Gets and returns a region, if any, that has the specified <paramref name="id" />.
        /// </summary>
        /// <param name="id">The region identifier to get for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the region matching the specified <paramref name="regionId" /> if it exists.
        /// </returns>
        public async Task<RegionDto> GetRegionByIdAsync(long id)
        {
            var regionFromDb = await _regionRepository.GetByIdAsync(id);

            if (regionFromDb is null)
                throw new NotFoundException($"The region with id: {id} doesn't exist in the database.");

            var regionDto = _mapper.Map<Region, RegionDto>(regionFromDb!);

            return regionDto;
        }

        /// <summary>
        /// Gets and returns a list of cities.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the list of regions.
        /// </returns>
        public async Task<IEnumerable<CityDto>> GetCitiesAsync()
        {
            var citiesFromDb = await _cityRepository.GetAllAsync();
            var citiesDtos = _mapper.Map<IEnumerable<City>, IEnumerable<CityDto>>(citiesFromDb);

            return citiesDtos;
        }

        /// <summary>
        /// Gets and returns a city, if any, that has the specified <paramref name="id" />.
        /// </summary>
        /// <param name="id">The city identifier to get for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the region matching the specified <paramref name="cityId" /> if it exists.
        /// </returns>
        public async Task<CityDto> GetCityByIdAsync(long id)
        {
            var cityFromDb = await _cityRepository.GetByIdAsync(id);

            if (cityFromDb is null)
                throw new NotFoundException($"The city with id: {id} doesn't exist in the database.");

            var cityDto = _mapper.Map<City, CityDto>(cityFromDb!);

            return cityDto;
        }

        /// <summary>
        /// Gets and returns a list of genders.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the list of genders.
        /// </returns>
        public async Task<IEnumerable<GenderDto>> GetGendersAsync()
        {
            var gendersFromDb = await _genderRepository.GetAllAsync();
            var gendersDto = _mapper.Map<IEnumerable<Gender>, IEnumerable<GenderDto>>(gendersFromDb);

            return gendersDto;
        }

        /// <summary>
        /// Gets and returns a gender, if any, that has the specified <paramref name="id" />.
        /// </summary>
        /// <param name="id">The gender identifier to get for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the gender matching the specified <paramref name="genderId" /> if it exists.
        /// </returns>
        public async Task<GenderDto> GetGenderByIdAsync(long id)
        {
            var genderFromDb = await _genderRepository.GetByIdAsync(id);

            if (genderFromDb is null)
                throw new NotFoundException($"The gender with id: {id} doesn't exist in the database.");

            var genderDto = _mapper.Map<Gender, GenderDto>(genderFromDb!);

            return genderDto;
        }

        /// <summary>
        /// Gets and returns a list of statuses.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the list of statuses.
        /// </returns>
        public async Task<IEnumerable<StatusDto>> GetStatusesAsync()
        {
            var statusesFromDb = await _statusRepository.GetAllAsync();
            var statusesDto = _mapper.Map<IEnumerable<Status>, IEnumerable<StatusDto>>(statusesFromDb);

            return statusesDto;
        }


        /// <summary>
        /// Gets and returns a status, if any, that has the specified <paramref name="id" />.
        /// </summary>
        /// <param name="id">The status identifier to get for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the status matching the specified <paramref name="statusId" /> if it exists.
        /// </returns>
        public async Task<StatusDto> GetStatusByIdAsync(long id)
        {
            var statusFromDb = await _statusRepository.GetByIdAsync(id);

            if (statusFromDb is null)
                throw new NotFoundException($"The status with id: {id} doesn't exist in the database.");

            var statusDto = _mapper.Map<Status, StatusDto>(statusFromDb!);

            return statusDto;
        }

        #endregion Get methods
    }
}

