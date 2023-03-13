using Microsoft.AspNetCore.Identity;
using MobvenSozluk.Domain.Concrete.Entities;
using MobvenSozluk.Infrastructure.Exceptions;
using MobvenSozluk.Repository.DTOs.CustomQueryDTOs;
using MobvenSozluk.Repository.DTOs.EntityDTOs;
using MobvenSozluk.Repository.DTOs.ResponseDTOs;
using MobvenSozluk.Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;


        public AccountService(SignInManager<User> signInManager, UserManager<User> userManager, ITokenService tokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<CustomResponseDto<UserDtoWithToken>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if(user == null)
            {
                throw new NotFoundException($"{typeof(User).Name} not found");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if(!result.Succeeded)
            {
                throw new NotFoundException($"User name or password wrong");
            }

            var loggedInUser = new UserDtoWithToken
            {
                Email = user.Email,
                Token = await _tokenService.CreateToken(user),
                Name = user.UserName
            };

            return CustomResponseDto<UserDtoWithToken>.Success(200, loggedInUser);

        }

        public async Task<CustomResponseDto<UserDtoWithToken>> Register(RegisterDto registerDto)
        {
            var user = new User
            {
                UserName = registerDto.Name,
                Email = registerDto.Email
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if(!result.Succeeded)
            {
                throw new NotFoundException($"Something went wrong");
            }

            var roleResult = await _userManager.AddToRoleAsync(user, "User");

            if (!roleResult.Succeeded)
            {
                throw new NotFoundException($"Something went wrong");
            }

            var registeredUser = new UserDtoWithToken
            {
                Name = user.UserName,
                Token = await _tokenService.CreateToken(user),
                Email = user.Email
            };

            return CustomResponseDto<UserDtoWithToken>.Success(200, registeredUser);

        }
    }
}
