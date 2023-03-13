using Microsoft.OpenApi.Models;

namespace MobvenSozluk.API.Extensions
{
    public static class SwaggerServiceExtensions
    {
        #region CODE EXPLANATION SECTION
        /*
         "services.AddEndpointsApiExplorer()" 
         method registers the Microsoft.AspNetCore.Mvc.ApiExplorer service,
         which is required to discover the API's controllers and generate OpenAPI documents.

         "services.AddSwaggerGen()" method loads the Swashbuckle.AspNetCore dependency required for Swagger and configures Swagger.
         Inside this method, the "OpenApiSecurityScheme" class is used to define a security scheme, which defines an authentication method such as JWT to be used for testing with the "Authorize" button in Swagger.

         Security scheme created with "AddSecurityDefinition()" is specified with AddSecurityRequirement() to restrict API access through Swagger UI.
         */
        #endregion
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "Mobven Sozluk Authorizations",
                    Name = "Authorisation",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer", // Alternatives =>> Digest, Basic
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                c.AddSecurityDefinition("Bearer", securitySchema);

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    {
                        securitySchema, new[] {"Bearer"}
                    }
                };

                c.AddSecurityRequirement(securityRequirement);

            });
            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            return app;
        }
    }
}
