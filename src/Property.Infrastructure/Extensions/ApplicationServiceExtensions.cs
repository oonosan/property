using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Property.ApplicationCore.Interfaces.Services;
using Property.Infrastructure.Data.Context;
using Property.Services.Services;

namespace Property.Infrastructure.Extensions
{
    public  static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Connect to database
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<PropertyDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            // AddSingleton: When a Singleton is created/instantiated it doesn't stop until our application stops
            // AddScoped: is scoped to the lifetime of the HTTP request. When a request comes in and we have a service injected into a particullar controller,
            //            then a new instance of the service is created and when the request is finished, the service is disposed
            // AddTransient: the service is going to be created and destroyed as soons as the method is finished. Not quite right for a HTTP request
            // JWT
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
