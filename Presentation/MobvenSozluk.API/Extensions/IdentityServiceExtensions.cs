using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MobvenSozluk.Domain.Concrete.Entities;
using MobvenSozluk.Persistance.Context;
using System.Text;

namespace MobvenSozluk.API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {

            //JWT token authentication configuration file
            //Warning: If you add new lines to code screen you should update "regions" to make code more understandable.

            var builder = services.AddIdentityCore<User>(); 
            builder = new IdentityBuilder(builder.UserType, builder.Services); 
            builder.AddRoles<Role>(); 
            builder.AddRoleManager<RoleManager<Role>>(); 
            builder.AddEntityFrameworkStores<AppDbContext>(); 
            builder.AddSignInManager<SignInManager<User>>();

            #region CODE EXPLANATION SECTION 1
            /*
             * Define Identity with user with                                     "services.AddIdentityCore<User>()"
             * Create identity builder instance to add features.                  "new IdentityBuilder(builder.UserType, builder.Services)"
             * Configure role.                                                    "builder.AddRoles<Role>()"        
             * Configure role manager which has provided from Identity library.   "AddRoleManager<RoleManager<Role>>();"
             * Add entity framework stores for the identity data using.           "AddEntityFrameworkStores<AppDbContext>();"
             * Configure signin manager which will used by sign in operations.    "AddSignInManager<SignInManager<User>>();"
             */
            #endregion


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

            #region CODE EXPLANATION SECTION 2
            /*
             * Add JWT bearer authentication with "JwtBearerDefaults.AuthenticationScheme"
             * Configure options for Jwt authentication with "TokenValidationParameters"
             * Specify what Jwt token needs
             * In IssuerSigningKey; we are configuring symmetricSecurityKey with converting bits to bytes which we have given random string; check appsettings.json
             * ValidateIssuer; means that indicates who produced the token
             * LifetimeValidator; If tokenexpire date has passed than token loses the validity.
             */
            #endregion

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
                opt.AddPolicy("RequireEditorRole", policy => policy.RequireRole("Editor"));
                opt.AddPolicy("RequireUserRole", policy => policy.RequireRole("User"));

            });

            #region CODE EXPLANATION SECTION 3
            /*
             * Add Authorization policies with "AddAuthorization(opt => {})
             * The policies are used to restrict access to specific parts of the application based on the user's roles
             * If you want to check how we can use that policy attributes then you can check "TestController".
             */
            #endregion

            return services;
        }

        

    }
}

