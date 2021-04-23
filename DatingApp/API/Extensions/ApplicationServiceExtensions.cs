using API.Data;
using API.Helpers;
using API.Interfaces;
using API.Services;
using DatingApp.API.Data;
using DatingApp.API.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServiceExtensionss
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            
            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IPhotoService, PhotoService>();

            services.AddScoped<ILikeRepository, LikeRepository>();

            services.AddScoped<IMessagesRepository, MessagesRepository>();

            services.AddScoped<LogUserActivity>();

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("SqliteConnection"));
            });

            return services;
        }
    }
}