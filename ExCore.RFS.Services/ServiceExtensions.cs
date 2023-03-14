using Microsoft.Extensions.DependencyInjection;
namespace ExCore.RFS.Services
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ServiceExtension(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            return services;
        }
    }
}
