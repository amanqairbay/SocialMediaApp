﻿using System;
using AutoMapper;
using Core.DTOs.User;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web.API.Extensions;
using Web.API.Helpers;
using Core.RequestFeatures;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UsersController(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery]UserParameters userParameters)
        {
            var currentUserId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            userParameters.UserId = currentUserId;

            var users = await _userManager.GetPagedListUsersAsync(userParameters);
            //if (string.IsNullOrEmpty(userParameters.Ge))

            //var users = await _userManager.GetAllUsersAsync();
            var usersDto = _mapper.Map<IEnumerable<UserForListDto>>(users);

            Response.AddPagination(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);

            return Ok(usersDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(long id)
        {
            var user = await _userManager.GetUserByIdAsync(id);
            var userDto = _mapper.Map<AppUser, UserForDetailedDto>(user!);

            return Ok(userDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(long id, UserForUpdateDto userForUpdateDto)
        {
            if (id != long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value))
                return Unauthorized();

            var userFromDb = await _userManager.GetUserByIdAsync(id);

            var userDto = _mapper.Map(userForUpdateDto, userFromDb);

            var result = await _userManager.UpdateAsync(userDto!);

            if (result.Succeeded) return NoContent();

            throw new Exception($"Updating user {id} failed on save");
        }
    }
}

