using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Property.ApplicationCore.Interfaces.Services;
using Property.Infrastructure.Data.Context;
using Property.Services.Services;

namespace Property.WebAPI
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCors();

            // Connect to database
            string connectionString = _config.GetConnectionString("DefaultConnection");
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // forces HTTP calls to automatically redirect to equivalent HTTPS addresses
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

            // used to enable authentication and then subsequently allow authorization
            // app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
