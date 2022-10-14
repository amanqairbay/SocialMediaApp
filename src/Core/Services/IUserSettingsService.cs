using Core.DTOs;

namespace Core.Services
{
    /// <summary>
    /// Represents the user settings.
    /// </summary>
    public interface IUserSettingsService
    {
        /// <summary>
        /// Gets and returns a list of regions.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the list of regions.
        /// </returns>
        Task<IEnumerable<RegionDto>> GetRegionsAsync();

        /// <summary>
        /// Gets and returns a region, if any, that has the specified <paramref name="id" />.
        /// </summary>
        /// <param name="id">The region identifier to get for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the region matching the specified <paramref name="id" /> if it exists.
        /// </returns>
        Task<RegionDto> GetRegionByIdAsync(long id);

        /// <summary>
        /// Gets and returns a list of cities.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the list of regions.
        /// </returns>
        Task<IEnumerable<CityDto>> GetCitiesAsync();

        /// <summary>
        /// Gets and returns a city, if any, that has the specified <paramref name="id" />.
        /// </summary>
        /// <param name="id">The city identifier to get for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the region matching the specified <paramref name="id" /> if it exists.
        /// </returns>
        Task<CityDto> GetCityByIdAsync(long id);

        /// <summary>
        /// Gets and returns a list of genders.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the list of genders.
        /// </returns>
        Task<IEnumerable<GenderDto>> GetGendersAsync();

        /// <summary>
        /// Gets and returns a gender, if any, that has the specified <paramref name="id" />.
        /// </summary>
        /// <param name="id">The gender identifier to get for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the gender matching the specified <paramref name="id" /> if it exists.
        /// </returns>
        Task<GenderDto> GetGenderByIdAsync(long id);

        /// <summary>
        /// Gets and returns a list of statuses.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the list of statuses.
        /// </returns>
        Task<IEnumerable<StatusDto>> GetStatusesAsync();

        /// <summary>
        /// Gets and returns a status, if any, that has the specified <paramref name="id" />.
        /// </summary>
        /// <param name="id">The status identifier to get for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the status matching the specified <paramref name="id" /> if it exists.
        /// </returns>
        Task<StatusDto> GetStatusByIdAsync(long id);
    }
}

