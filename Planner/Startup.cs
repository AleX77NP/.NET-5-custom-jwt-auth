using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Planner.Data;
using Microsoft.EntityFrameworkCore;
using Planner.Helpers;
using Planner.Authorization;

namespace Planner
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            var serverVersion = new MySqlServerVersion(new Version(8, 0, 25));

            services.AddControllers();
            services.AddDbContext<UserContext>(
                dbContextOptions => dbContextOptions
                .UseMySql(Configuration.GetConnectionString("Default"), serverVersion)
                .EnableSensitiveDataLogging() // <-- These two calls are optional but help
                .EnableDetailedErrors()
           );

            services.AddScoped<IUserRepository, UserRepositoty>();
            services.AddScoped<JwtService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(options => options
            .WithOrigins(new[]{ "http://localhost:3000", "http://localhost:8080"})
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            );

            app.UseMiddleware<JwtMiddleware>();

           // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
