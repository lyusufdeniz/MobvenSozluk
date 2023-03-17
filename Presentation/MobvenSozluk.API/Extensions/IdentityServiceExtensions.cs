using Amazon.Runtime.Internal;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MobvenSozluk.Domain.Concrete.Entities;
using MobvenSozluk.Persistance.Context;
using MongoDB.Libmongocrypt;
using System.Data;
using System.Security.Policy;
using System.Text;

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

