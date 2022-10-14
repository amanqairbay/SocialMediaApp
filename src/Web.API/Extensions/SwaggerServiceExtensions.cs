using System;
using System.Reflection;
using CloudinaryDotNet.Actions;
using Microsoft.OpenApi.Models;

namespace Web.API.Extensions
{
    /// <summary>
    /// Swagger service extensions
    /// </summary>
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Social Media App API",
                    Version = "v1",
                    Description = "Social Media App API by Aman Qairbay",
                    TermsOfService = new Uri("https://github.com/amanqairbay/SocialMediaApp"),
                    Contact = new OpenApiContact
                    {
                        Name = "Aman Qairbay",
                        Email = "amanqairbay@gmail.com",
                        Url = new Uri("https://github.com/amanqairbay")
                    }
                });

                var xmlFile = $"{typeof(Program).GetTypeInfo().Assembly.GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Auth Bearer Schema",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                c.AddSecurityDefinition("Bearer", securitySchema);
                var securityRequirement = new OpenApiSecurityRequirement { { securitySchema, new[] { "Bearer" } } };
                c.AddSecurityRequirement(securityRequirement);
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SocialMedia API v1"));

            return app;
        }
    }
}

