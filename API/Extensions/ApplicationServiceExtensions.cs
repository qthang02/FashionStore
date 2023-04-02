using API.Config;
using API.Services;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<UsersApiOptions>(config.GetSection("UsersApiOptions"));
        services.AddTransient<IUserService, UserService>();
        services.AddHttpClient<IUserService, UserService>();

        return services;
    }
}