using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;


namespace KocUniversityCourseManagement.Infrastructure
{
    public static class RedisExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("RedisConnection");
            });


            return services;
        }
    }
}
