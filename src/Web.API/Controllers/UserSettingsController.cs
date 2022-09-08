using System;
using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers
{
    public class UserSettingsController : BaseApiController
    {
        private readonly IRepository<Region> _regionRepository;
        private readonly IRepository<City> _cityRepository;
        private readonly IRepository<Gender> _genderRepository;
        private readonly IRepository<Status> _statusRepository;
        private readonly IMapper _mapper;

        public UserSettingsController(
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

        [HttpGet("regions")]
        public async Task<ActionResult<IReadOnlyList<RegionDto>>> GetRegionsAsync()
        {
            var regions = await _regionRepository.GetAllAsync();
            var regionsDto = _mapper.Map<IReadOnlyList<Region>, IReadOnlyList<RegionDto>>(regions);

            return Ok(regionsDto);
        }

        [HttpGet("regions/{id}")]
        public async Task<ActionResult<RegionDto>> GetRegionByIdAsync(long id)
        {
            var region = await _regionRepository.GetByIdAsync(id);

            if (region is null)
                throw new NotFoundException($"The region with id: {id} doesn't exist in the database.");

            var regionDto = _mapper.Map<Region, RegionDto>(region!);

            return regionDto;
        }

        [HttpGet("cities")]
        public async Task<ActionResult<IReadOnlyList<CityDto>>> GetCitiesAsync()
        {
            var cities = await _cityRepository.GetAllAsync();
            var citiesDto = _mapper.Map<IReadOnlyList<City>, IReadOnlyList<CityDto>>(cities);

            return Ok(citiesDto);
        }

        [HttpGet("cities/{id}")]
        public async Task<ActionResult<CityDto>> GetCityByIdAsync(long id)
        {
            var city = await _cityRepository.GetByIdAsync(id);

            if (city is null)
                throw new NotFoundException($"The city with id: {id} doesn't exist in the database.");

            var cityDto = _mapper.Map<City, CityDto>(city!);

            return cityDto;
        }

        [HttpGet("genders")]
        public async Task<ActionResult<Gender>> GetGenders()
        {
            var genders = await _genderRepository.GetAllAsync();

            return Ok(genders);
        }

        [HttpGet("genders/{id}")]
        public async Task<ActionResult<Gender>> GetGenderByIdAsync(long id)
        {
            var gender = await _genderRepository.GetByIdAsync(id);

            if (gender is null)
                throw new NotFoundException($"The gender with id: {id} doesn't exist in the database.");

            return gender;
        }

        [HttpGet("statuses")]
        public async Task<ActionResult<Status>> GetStatuses()
        {
            var statuses = await _statusRepository.GetAllAsync();

            return Ok(statuses);
        }

        [HttpGet("statuses/{id}")]
        public async Task<ActionResult<Status>> GetStatusesByIdAsync(long id)
        {
            var status = await _statusRepository.GetByIdAsync(id);

            if (status is null)
                throw new NotFoundException($"The status with id: {id} doesn't exist in the database.");

            return status;
        }
    }
}

