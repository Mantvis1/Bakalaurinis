﻿using bakalaurinis.Dtos.User;
using bakalaurinis.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace bakalaurinis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        [Produces(typeof(AfterAutenticationDto))]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateDto authenticateDto)
        {
            var user = await _userService.Authenticate(authenticateDto);

            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [Produces(typeof(bool))]
        public async Task<IActionResult> Register([FromBody]RegistrationDto registrationDto)
        {
            try
            {
                await _userService.Register(registrationDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return Ok();
        }

        [HttpGet("self/{id}")]
        [Produces(typeof(UserNameDto))]
        public async Task<IActionResult> GetUsername(int id)
        {
            var username = await _userService.GetNameById(id);

            if (username == null)
            {
                return NotFound();
            }

            return Ok(username);
        }

        [HttpDelete("{id}")]
        [Produces(typeof(bool))]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _userService.Delete(id);

            return Ok(isDeleted);
        }

        [HttpGet("status/{id}")]
        [Produces(typeof(GetScheduleStatus))]
        public async Task<IActionResult> GetStatus(int id)
        {
            var userStatus = await _userService.GetStatusById(id);

            if (userStatus == null)
            {
                return NotFound();
            }

            return Ok(userStatus);
        }
    }
}
