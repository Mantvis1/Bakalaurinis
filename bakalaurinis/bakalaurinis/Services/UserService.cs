using AutoMapper;
using bakalaurinis.Configurations;
using bakalaurinis.Dtos.User;
using bakalaurinis.Infrastructure.Database.Models;
using bakalaurinis.Infrastructure.Repositories.Interfaces;
using bakalaurinis.Services.Interfaces;
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
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        private List<User> _users = new List<User>
        {
            new User { Id = 1, Username = "test", Password = "test" }
        };

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings, IMapper mapper, IUserRepository userRepository)
        {
            _appSettings = appSettings.Value;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<AfterAutentificationDto> Authenticate(string username, string password)
        {
            var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);

            var afterAuthDto = _mapper.Map<AfterAutentificationDto>(user);

            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            afterAuthDto.Token = tokenHandler.WriteToken(token);
            afterAuthDto.Id = user.Id;

            return afterAuthDto;
        }

        public async Task<int> Register(RegistrationDto registrationDto)
        {
            if (registrationDto == null)
            {
                throw new ArgumentNullException();
            }

            var user = _mapper.Map<User>(registrationDto);

            return await _userRepository.Create(user);
        }

        public async Task<UserNameDto> GetNameById(int id)
        {
            //  var user = _users.SingleOrDefault(x => x.Id == id);
            var userNameDto = _mapper.Map<UserNameDto>(new User { Id = 1, Username = "test", Password = "test" });

            return userNameDto;
        }
    }
}
