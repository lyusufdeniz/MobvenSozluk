using Amazon.Runtime;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MobvenSozluk.Domain.Concrete.Entities;
using MobvenSozluk.Infrastructure.Services;
using MobvenSozluk.Repository.DTOs.EntityDTOs;
using MobvenSozluk.Repository.DTOs.ResponseDTOs;
using MobvenSozluk.Repository.Services;
using MongoDB.Bson.IO;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;

namespace MobvenSozluk.API.Controllers
{
    public class AccountController : CustomBaseController
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public AccountController(IAccountService accountService, IMapper mapper, ITokenService tokenService)
        {
            _accountService = accountService;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var response = await _accountService.Login(loginDto);

            return CreateActionResult(response);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var token = Request.Cookies["BearerToken"];
            if (token != null)
            {
                var userId = _tokenService.ValidateToken(token);

                var response = await _accountService.Logout(userId);

                return CreateActionResult(response);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            return CreateActionResult(await _accountService.Register(registerDto));
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody]RefreshTokenDto token)
        {
            return CreateActionResult(await _accountService.RefreshToken(token));
        }
    }
}
