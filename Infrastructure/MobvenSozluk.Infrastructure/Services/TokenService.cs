using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MobvenSozluk.Domain.Concrete.Entities;
using MobvenSozluk.Repository.Services;
using MobvenSozluk.Repository.UnitOfWorks;
using System;
using System.Collections.Generic;
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

      
        #region CODE EXPLANATION SECTION 1
        /*
          A token should be signed to ensure its authenticity and integrity.
          To do so Jwt uses "SymmetricSecurityKey" function to create key; after that the key will signed by "HMAC" algorithm
          "Encoding.UTF8.GetBytes(_config["Token:Key"])" this means changing bits to bytes which string have given from config.
              That string can me meanless string; it is not important to means anything. Program only needs any string to use it.
              Why we encode as UTF8; because HMAC signes tokens with only byte version.
         */
        #endregion

        public async Task<string> CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(user);

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            #region CODE EXPLANATION SECTION 2
            /*
              The CreateToken method takes in a User object and generates a JWT token using the claims and signing credentials provided.
              Claims are used to store information about the user such as their email, name, and roles.
              In this method, the email and name are added as claims, as well as any roles associated with the user.
             */
            #endregion

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            #region CODE EXPLANATION SECTION 3
            /*
              Hash-based Message Authentication Code
              HMAC is a symmetric key encryption algorithm, meaning the same key is used for both signing and verifying.
              We could use RSA Rivest-Shamir-Adleman algorithm but it works with 2 different key while signing and verifying.
             */
            #endregion

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

            #region CODE EXPLANATION SECTION 4
            /*
               Create token descriptor with "new SecurityTokenDecriptor" method. 
                      Configuring Subject; which needs user info , Expires; which is expire date of token, 
                      SigningCredential; which has signed key, Issuer; which specifies who created token.
               Create tokenHandler object to create token with tokenDescriptor
               Return token with WriteToken method
             */
            #endregion
        }

        public async Task SetRefreshToken(RefreshToken refreshToken, User user)
        {
            user.RefreshToken = refreshToken.Token;
            user.RefreshTokenCreated = refreshToken.Created;
            user.RefreshTokenExpires = refreshToken.Expires;
            await _unitOfWork.CommitAsync();

            #region CODE EXPLANATION SECTION 5
            /*
               Refresh token information must be stored anywhere, it was saved in the user database.
               This method is used to update existing user refreshtoken.
             */
            #endregion
        }

        public RefreshToken CreateRefreshToken()
        {
            var expiresInDays = Convert.ToDouble(_config["Token:RefreshTokenExpireInDays"]);
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.UtcNow.AddDays(expiresInDays),
                Created = DateTime.UtcNow
            };

            return refreshToken;

            #region CODE EXPLANATION SECTION 6
            /*
               Token: A new random 64-byte string is generated and converted to a Base64-encoded string using Convert.ToBase64String.
               Expires: The resulting DateTime object represents the expiration time of the refresh token.
               Created: This property is used to keep track of when the refresh token was created.
               We use UTC now  This is because UTC time is a standard reference time and it avoids issues related to different
                      time zones and daylight saving time changes.
             */
            #endregion
        }
    }
}
