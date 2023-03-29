using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MobvenSozluk.Domain.Concrete.Entities;
using MobvenSozluk.Domain.Constants;
using MobvenSozluk.Infrastructure.Exceptions;
using MobvenSozluk.Repository.Services;
using MobvenSozluk.Repository.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Infrastructure.Services
{

    public class TokenService : ITokenService
    {

        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        public TokenService(IConfiguration config, UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]));
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var roles = await _userManager.GetRolesAsync(user);

            if (roles == null)
            {
                throw new NotFoundException(MagicStrings.RoleNotExist);
            }

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var expiresInDays = Convert.ToDouble(_config["Token:AccessTokenExpireInDays"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(expiresInDays),
                SigningCredentials = creds,
                Issuer = _config["Token:Issuer"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    
        public async Task SetRefreshToken(RefreshToken refreshToken, User user)
        {
            user.RefreshToken = refreshToken.Token;
            user.RefreshTokenCreated = refreshToken.Created;
            user.RefreshTokenExpires = refreshToken.Expires;
            try
            {
                await _unitOfWork.CommitAsync();
            }
            catch 
            {
                throw new Exception(MagicStrings.BadRequestDescription);
            }
        }

        public Task<RefreshToken> CreateRefreshToken()
        {
            var expiresInDays = Convert.ToDouble(_config["Token:RefreshTokenExpireInDays"]);
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.UtcNow.AddDays(expiresInDays),
                Created = DateTime.UtcNow
            };

            return Task.FromResult(refreshToken);
        }

        public Task<string?> FindUserByToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var decodedToken = handler.ReadJwtToken(token);
            var decodedClaims = decodedToken.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;
            return Task.FromResult(decodedClaims);

        }
    }
}
