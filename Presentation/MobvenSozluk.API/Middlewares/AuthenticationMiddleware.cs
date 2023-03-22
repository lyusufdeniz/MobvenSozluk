using MobvenSozluk.Repository.DTOs.EntityDTOs;
using MobvenSozluk.Repository.Services;
using System.IdentityModel.Tokens.Jwt;

namespace MobvenSozluk.API.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ITokenService tokenService, IAccountService accountService)
        {
            var bearerTokenCookie = context.Request.Cookies["BearerToken"];
            var refreshTokenCookie = context.Request.Cookies["refreshToken"];

            if (context.Request.Path == "/api/Account/login" || context.Request.Path == "/api/Account/logout")
            {
                await _next.Invoke(context);
            }
            else if (bearerTokenCookie != null && refreshTokenCookie != null) 
            {
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(bearerTokenCookie);
                var exp = token.Claims.FirstOrDefault(c => c.Type == "exp").Value;
                if (exp != null)
                {
                    var expTime = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(exp)).UtcDateTime;
                    if (expTime < DateTime.UtcNow)
                    {
                        var tokenCookieRefresh = new RefreshTokenDto
                        {
                            RefreshToken = refreshTokenCookie
                        };
                        var response = await accountService.RefreshToken(tokenCookieRefresh);
                        context.Response.Cookies.Append("BearerToken", response.Data.Token);
                        context.Response.Cookies.Append("refreshToken", response.Data.RefreshToken);
                        context.Request.Headers.Add("Authorization", "Bearer " + response.Data.Token);
                        await _next.Invoke(context);
                    }
                    else
                    {
                        context.Request.Headers.Add("Authorization", "Bearer " + bearerTokenCookie);
                        await _next.Invoke(context);
                    }
                }
                else
                {
                    throw new Exception("Something went wrong");
                }
            }   
            else
            {
                await _next.Invoke(context);
            }
        }
    }
}
