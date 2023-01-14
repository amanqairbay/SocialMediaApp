using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Errors;
using Core.Interfaces;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers
{
    public class UserSettingsController : BaseApiController
    {
        private readonly IUserSettingsService _userSettingsService;

        public UserSettingsController(IUserSettingsService userSettingsService)
        {
            _userSettingsService = userSettingsService;
        }

        /// <summary>
        /// Gets and returns a list of regions.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the list of regions.
        /// </returns>
        /// <response code="200">If all regions successfully returned.</response>
        [HttpGet("regions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRegionsAsync()
        {
            var regions = await _userSettingsService.GetRegionsAsync();

            return Ok(regions);
        }

        /// <summary>
        /// Gets and returns a region, if any, that has the specified <paramref name="id" />.
        /// </summary>
        /// <param name="id">The region identifier to get for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the region matching the specified <paramref name="id" /> if it exists.
        /// </returns>
        /// <response code="200">If the region successfully returned.</response>
        /// <response code="404">If the region doesn't exist.</response>
        [HttpGet("regions/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRegionByIdAsync(long id)
        {
            var region = await _userSettingsService.GetRegionByIdAsync(id);

            return Ok(region);
        }

        /// <summary>
        /// Gets and returns a list of cities/>.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the list of cities.
        /// </returns>
        /// <response code="200">If all cities successfully returned.</response>
        [HttpGet("cities")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCitiesAsync()
        {
            var cities = await _userSettingsService.GetCitiesAsync();

            return Ok(cities);
        }

        /// <summary>
        /// Gets and returns a city, if any, that has the specified <paramref name="id" />.
        /// </summary>
        /// <param name="id">The city identifier to get for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the region matching the specified <paramref name="id" /> if it exists.
        /// </returns>
        /// <response code="200">If the city successfully returned.</response>
        /// <response code="404">If the city doesn't exist.</response>
        [HttpGet("cities/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCityByIdAsync(long id)
        {
            var city = await _userSettingsService.GetCityByIdAsync(id);

            return Ok(city);
        }

        /// <summary>
        /// Gets and returns a list of genders.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the list of genders.
        /// </returns>
        /// <response code="200">If all genders successfully returned.</response>
        [HttpGet("genders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Gender>> GetGendersAsync()
        {
            var genders = await _userSettingsService.GetGendersAsync();

            return Ok(genders);
        }

        /// <summary>
        /// Gets and returns a gender, if any, that has the specified <paramref name="id" />.
        /// </summary>
        /// <param name="id">The gender identifier to get for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the gender matching the specified <paramref name="id" /> if it exists.
        /// </returns>
        /// <response code="200">If the gender successfully returned.</response>
        /// <response code="404">If the gender doesn't exist.</response>
        [HttpGet("genders/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetGenderByIdAsync(long id)
        {
            var gender = await _userSettingsService.GetGenderByIdAsync(id);

            return Ok(gender);
        }

        /// <summary>
        /// Gets and returns a list of statuses.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the list of statuses.
        /// </returns>
        /// <response code="200">If all statuses successfully returned.</response>
        [HttpGet("statuses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Status>> GetStatusesAsync()
        {
            var statuses = await _userSettingsService.GetStatusesAsync();

            return Ok(statuses);
        }

        /// <summary>
        /// Gets and returns a status, if any, that has the specified <paramref name="id" />.
        /// </summary>
        /// <param name="id">The status identifier to get for.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the status matching the specified <paramref name="id" /> if it exists.
        /// </returns>
        /// <response code="200">If the status successfully returned.</response>
        /// <response code="404">If the status doesn't exist.</response>
        [HttpGet("statuses/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Status>> GetStatusesByIdAsync(long id)
        {
            var status = await _userSettingsService.GetStatusByIdAsync(id);

            return Ok(status);
        }
    }
}

