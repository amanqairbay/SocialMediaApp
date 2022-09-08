using System;
using Core.Interfaces;
using Core.Services;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Services;
using Web.API.Helpers;

namespace Web.API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.ConfigureCors();
            services.ConfigureSqlContext(configuration);
            services.ConfigureCloudinary(configuration);
            services.AddSingleton<ILoggerManager, LoggerManager>();
            services.AddSingleton<ITokenService, TokenService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IPhotoRepository, PhotoRepository>();
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddIdentityServices(configuration);
            services.AddSwaggerDocumentation();
            services.AddControllers();
            // Must be after Addcontrollers()
            services.ConfigureValidationErrorResponse();
            return services;
        }
    }
}

