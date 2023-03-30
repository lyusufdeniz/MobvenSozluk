using Amazon.Runtime;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MobvenSozluk.API.Helpers;
using MobvenSozluk.Domain.Concrete.Entities;
using MobvenSozluk.Infrastructure.Services;
using MobvenSozluk.Repository.DTOs.EntityDTOs;
using MobvenSozluk.Repository.DTOs.ResponseDTOs;
using MobvenSozluk.Repository.Services;
using MongoDB.Bson.IO;
using Serilog;
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
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _config;

        public AccountController(IAccountService accountService, ITokenService tokenService, IConfiguration config)
        {
            _accountService = accountService;
            _tokenService = tokenService;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {

            var response = await _accountService.Login(loginDto);

            Response.SetCookie("BearerToken", response.Data.Token, DateTime.UtcNow.AddDays(Convert.ToDouble(_config["Token:AccessTokenExpireInDays"])));
            Response.SetCookie("refreshToken", response.Data.RefreshToken, DateTime.UtcNow.AddDays(Convert.ToDouble(_config["Token:RefreshTokenExpireInDays"])));

            return CreateActionResult(response);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var token = Request.Cookies["BearerToken"];
            var userId = _tokenService.FindUserByToken(token);
            var response = await _accountService.Logout(userId.Result);
            Response.SetCookie("BearerToken", response.Data.AccessToken, DateTime.UtcNow.AddDays(Convert.ToDouble(_config["Token:LogoutToken"])));
            Response.SetCookie("refreshToken", response.Data.RefreshToken, DateTime.UtcNow.AddDays(Convert.ToDouble(_config["Token:LogoutToken"])));

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
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
