namespace API
{
    using System.Text;

    using Application;
    using Application.Activities;
    using Application.Activities.Queries;
    using Application.Activities.Validators;

    using Domain;
    using Domain.Repositories;

    using FluentValidation.AspNetCore;

    using Infrastructure.Security;

    using MediatR;

    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.Authorization;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.IdentityModel.Tokens;

    using Middleware;

    using Persistence;
    using Persistence.Repositories;

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
            services.AddMvc(
                    opt =>
                    {
                        var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                        opt.Filters.Add(new AuthorizeFilter(policy));
                    })
                .AddFluentValidation(
                    cfg =>
                    {
                        cfg.RegisterValidatorsFromAssemblyContaining<CreateCommandValidator>();
                    });
            services.AddMediatR(typeof(ActivitiesListQueryHandler).Assembly);
            services.AddScoped<IActivitiesRepository, ActivitiesRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            var builder = services.AddIdentityCore<AppUser>();
            var identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);
            identityBuilder.AddEntityFrameworkStores<DataContext>();
            identityBuilder.AddSignInManager<SignInManager<AppUser>>();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenKey"]));
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateAudience = false,
                        ValidateIssuer = false
                    };
                });
            
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

            app.UseAuthentication();
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
