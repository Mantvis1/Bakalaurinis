using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace bakalaurinis.Configurations
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("holidays", new OpenApiInfo { Title = "Bakalaurinis", Version = "v1" });
            });

            return services;
        }

        public static void ConfigureAndUseSwagger(this IApplicationBuilder app)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/holidays/swagger.json", "??");
                options.RoutePrefix = "swagger";
            });
        }

    }
}
