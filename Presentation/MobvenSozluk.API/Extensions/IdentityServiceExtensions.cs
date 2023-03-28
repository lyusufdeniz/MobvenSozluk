using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MobvenSozluk.Domain.Concrete.Entities;
using MobvenSozluk.Persistance.Context;
using MongoDB.Libmongocrypt;
using Serilog;
using System;
using System.Data;
using System.Net;
using System.Text;
using System.Text.Json;

namespace MobvenSozluk.API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {

            var builder = services.AddIdentityCore<User>(); 
            builder = new IdentityBuilder(builder.UserType, builder.Services); 
            builder.AddRoles<Role>(); 
            builder.AddRoleManager<RoleManager<Role>>(); 
            builder.AddEntityFrameworkStores<AppDbContext>(); 
            builder.AddSignInManager<SignInManager<User>>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                  .AddJwtBearer(options => {
                      
                      options.TokenValidationParameters = new TokenValidationParameters
                      {
                          ValidateIssuerSigningKey = true,
                          IssuerSigningKey = new SymmetricSecurityKey(Encoding
                            .UTF8.GetBytes(config["Token:Key"])),
                          ValidIssuer = config["Token:Issuer"],
                          ValidateIssuer = true,
                          ValidateAudience = false,
                          LifetimeValidator = (notBefore, expires, securityToken, validationParameters) =>
                          {
                              if (expires != null)
                              {
                                  if (DateTime.UtcNow < expires)
                                  {
                                      return true;
                                  }
                              }
                              return false;
                          }
                      };
                      options.Events = new JwtBearerEvents()
                      {
                          OnChallenge = context =>
                          {
                              context.HandleResponse();
                              context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                              context.Response.ContentType = "application/json";

                              // Ensure we always have an error and error description.
                              if (string.IsNullOrEmpty(context.Error))
                                  context.Error = "invalid_token";
                              if (string.IsNullOrEmpty(context.ErrorDescription))
                                  context.ErrorDescription = "This request requires a valid JWT access token to be provided";


                              // Add some extra context for expired tokens.
                              if (context.AuthenticateFailure != null && context.AuthenticateFailure.GetType() == typeof(SecurityTokenExpiredException))
                              {
                                  var authenticationException = context.AuthenticateFailure as SecurityTokenExpiredException;
                                  context.Response.Headers.Add("x-token-expired", authenticationException.Expires.ToString("o"));
                                  context.ErrorDescription = $"The token expired on {authenticationException.Expires.ToString("o")}";


                              }



                              return context.Response.WriteAsync(JsonSerializer.Serialize(new
                              {
                                  error = context.Error,
                                  error_description = context.ErrorDescription
                              }));

                          },
                          OnForbidden = context =>
                          {

                              context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                              context.Response.ContentType = "application/json";
                              var message = "You are not authorized to access this resource";
                              var errorResponse = JsonSerializer.Serialize(new { message });
                              return context.Response.WriteAsync(errorResponse);
                          }
                      };
                      
                  });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
                opt.AddPolicy("RequireEditorRole", policy => policy.RequireRole("Editor"));
                opt.AddPolicy("RequireUserRole", policy => policy.RequireRole("User"));

            });  

            return services;
        }
    }
}

