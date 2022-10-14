using Core.Interfaces;
using Core.Services;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Services;
using Web.API.Helpers;

namespace Web.API.Extensions
{
    /// <summary>
    /// Represents the application service extensions.
    /// </summary>
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.ConfigureCors();
            services.ConfigureSqlContext(configuration);
            services.ConfigureCloudinary(configuration);
            services.AddSingleton<ILoggerManager, LoggerManager>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IUserSettingsService, UserSettingsService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IPhotoRepository, PhotoRepository>();
            services.AddScoped<LogUserActivity>();
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

