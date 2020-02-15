﻿using bakalaurinis.Configurations;
using bakalaurinis.Infrastructure.Database.Models;
using bakalaurinis.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace bakalaurinis.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AppSettings _appSettings;
        private readonly UserManager<User> _userManager;
        public AuthenticationService(IOptions<AppSettings> appSettings, UserManager<User> userManager)
        {
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        public async Task<User> Authenticate(string email, string password)
        {
            var userToVerify = await _userManager.FindByEmailAsync(email);

            if (userToVerify == null)
            {
                return null;
            }

            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                var user = await _userManager.Users.SingleAsync(x => x.Email == email);
                user.Client.Token = CreateJwt(user);

                return user;
            }

            return null;
        }

        private string CreateJwt(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(30);

            var token = new JwtSecurityToken(
                "Issuer",
                "Issuer",
                claims,
                expires: expires,
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
