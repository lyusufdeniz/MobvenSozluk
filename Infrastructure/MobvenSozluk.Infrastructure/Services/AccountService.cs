using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobvenSozluk.Domain.Concrete.Entities;
using MobvenSozluk.Infrastructure.Exceptions;
using MobvenSozluk.Repository.DTOs.CustomQueryDTOs;
using MobvenSozluk.Repository.DTOs.EntityDTOs;
using MobvenSozluk.Repository.DTOs.ResponseDTOs;
using MobvenSozluk.Repository.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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

        #region CODE EXPLANATION SECTION 1
        /*
          Configure "SignInManager", "UserManager" and "TokenService" with "User"
          
         */
        #endregion

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

            var refreshToken = new RefreshToken();
            await _tokenService.SetRefreshToken(refreshToken, user);

            var loggedInUser = new UserDtoWithToken
            {
                Email = user.Email,
                Token = await _tokenService.CreateToken(user),
                Name = user.UserName,
                RefreshToken = refreshToken.Token
            };     

            #region CODE EXPLANATION SECTION 2
            /*
              *Login method takes a LoginDto object as a parameter and first checks if a user with the given email exists in the database using the UserManager
                     If the user exists, it uses the SignInManager to check the validity of the given password. 
              * If the login is successful, it creates a new UserDtoWithToken object which includes the user's email, username,
                    and a JWT token that is created using the _tokenService.CreateToken method. 
             */
            #endregion

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

            #region CODE EXPLANATION SECTION 2
            /*
              *Register method takes a RegisterDto object as a parameter and creates a new User object using the UserManager.
              *It then uses the UserManager to create a new user in the database with the given name, email, and password.
              *It then adds the role of "User" to the new user using the UserManager; All new registered users will automatically assign "User" role. 
              *Finally, it creates a new UserDtoWithToken object which includes the user's email, username, 
                      and a JWT token that is created using the _tokenService.CreateToken method.
             */
            #endregion

            return CustomResponseDto<UserDtoWithToken>.Success(200, registeredUser);

        }

        public async Task<CustomResponseDto<UserDtoWithToken>> RefreshToken([FromBody] RefreshTokenDto token)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == token.RefreshToken);

            if (user != null && user?.RefreshTokenExpires > DateTime.UtcNow)
            {
                var newRefreshToken = new RefreshToken();
                await _tokenService.SetRefreshToken(newRefreshToken, user);

                var refreshTokenWithUser = new UserDtoWithToken
                {
                    Name = user.UserName,
                    Token = await _tokenService.CreateToken(user),
                    Email = user.Email,
                    RefreshToken = newRefreshToken.Token

                };


                return CustomResponseDto<UserDtoWithToken>.Success(200, refreshTokenWithUser);

            }
            else
            {
                throw new NotFoundException("User not found or token expired");
            }

            #region CODE EXPLANATION SECTION 3
            /*
               The method first queries the database to find the user associated with the refresh token
                   If a user is found and their RefreshTokenExpires property is greater than the current UTC time,
                   a new refresh token is generated and stored in the database using the _tokenService.SetRefreshToken method.
                The method then creates a new authentication token and refresh token using the _tokenService.CreateToken method
                   and setting the newRefreshToken with _tokenService.SetRefreshToken method
             */
            #endregion

        }
    }
}
