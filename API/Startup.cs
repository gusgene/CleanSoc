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

namespace API
{
    using Application.Activities;

    using Domain;

    using MediatR;

    using Microsoft.EntityFrameworkCore;

    using Persistence;

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
            services.AddDbContext<DataContext>(
                opt =>
                {
                    opt.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
                });
            services.AddCors(
                opt =>
                {
                    opt.AddPolicy("CorsPolicy",
                        policy =>
                        {
                            policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
                        });
                });
            services.AddControllers();
            services.AddMediatR(typeof(List.Handler).Assembly);
            services.AddScoped<IActivitiesRepository, ActivitiesRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
