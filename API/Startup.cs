namespace API
{
    using Application;
    using Application.Activities;
    using Application.Activities.Queries;
    using Application.Activities.Validators;

    using Domain;

    using FluentValidation.AspNetCore;

    using Infrastructure.Security;

    using MediatR;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using Middleware;

    using Persistence;

    public class Startup
    {
        #region Properties

        public IConfiguration Configuration { get; }

        #endregion

        #region Constructors

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion

        #region Methods

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
                    opt.AddPolicy(
                        "CorsPolicy",
                        policy =>
                        {
                            policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
                        });
                });
            services.AddControllers()
                .AddFluentValidation(
                    cfg =>
                    {
                        cfg.RegisterValidatorsFromAssemblyContaining<CreateCommandValidator>();
                    });
            services.AddMediatR(typeof(ActivitiesListQueryHandler).Assembly);
            services.AddScoped<IActivitiesRepository, ActivitiesRepository>();

            var builder = services.AddIdentityCore<AppUser>();
            var identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);
            identityBuilder.AddEntityFrameworkStores<DataContext>();
            identityBuilder.AddSignInManager<SignInManager<AppUser>>();

            services.AddAuthentication();
            services.AddScoped<IJwtGenerator, JwtGenerator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
            if (env.IsDevelopment())
            {
                // app.UseDeveloperExceptionPage();
                
            }
                

            //app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapControllers();
                });
        }

        #endregion
    }
}
