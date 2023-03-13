using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MobvenSozluk.Domain.Concrete.Entities;
using MobvenSozluk.Repository.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MobvenSozluk.Infrastructure.Services
{
    
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<User> _userManager;
        public TokenService(IConfiguration config, UserManager<User> userManager)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]));
            _userManager = userManager;
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


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
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
    }
}
