using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Demo.API.Models;
using Demo.Entities;
using Demo.Infrastructure;
using Demo.Infrastructure.Core.Helpers;
using Demo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;

namespace Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly IMapper _mapper;

        private readonly IAppSettings _appSettings;
        public UserController(
            IMapper mapper,
                IUserService userService,IAppSettings appSettings)
        {
            this._userService = userService;
            this._mapper = mapper;
            this._appSettings = appSettings;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]UserModel model)
        {
            if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
            {
                return BadRequest(new
                {
                    message = $"Incorrect user name and password"
                });
            }

            var user = await _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return Unauthorized();

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
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info (without password) and token to store client side
            return Ok(new
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = tokenString
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]UserModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    message = GetErrorMessageFromModalState(this.ModelState)
                });
            }

            // map dto to entity
            var user = _mapper.Map<User>(model);

            try
            {
                // save 
                _userService.Create(user, model.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(ex.Message);
            }
        }

        
        /// <summary>
        /// Get error message from model state.
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        protected string GetErrorMessageFromModalState(ModelStateDictionary modelState)
        {
            return string.Join("\n", modelState.Values.Select(x => string.Join(". ", x.Errors.Select(e => e.ErrorMessage))));
        }
    }
}
