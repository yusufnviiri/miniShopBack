using Contracts;
using Contracts.Repo;
using Contracts.Service;
using Entities.Models;
using LoggerService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Repository.context;
using Repository.Repos;
using saccoshop.ContextFactory;
using Services;
using Services.BusinessRules;
using System.Text;

namespace saccoshop.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
      services.AddCors(options =>
      {
          options.AddPolicy("CorsPolicy", builder =>
              builder.WithOrigins(
            "http://localhost:5173",
             "http://localhost:5174",
            "https://techforum.space",
            "https://www.buypamoja.com",
            "https://buypamoja.com"    
        )
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .WithExposedHeaders("X-Pagination"));
      });



        public static void ConfigureIISIntegration(this IServiceCollection services) =>
     services.Configure<IISOptions>(options =>
     {
     });
        public static void ConfigureLoggerService(this IServiceCollection services) => services.AddSingleton<ILoggerManager, LoggerManager>();
        public static void ConfigureRepositoryManager(this IServiceCollection services) => services.AddScoped<IRepositoryManager, RepositoryManager>();
        public static void ConfigureServiceManager(this IServiceCollection services) =>services.AddScoped<IServiceManager, ServiceManager>();

        public static void ConfigurePasswordHash(this IServiceCollection services)
        {
            services.Configure<PasswordHasherOptions>(options =>
            {
                options.IterationCount = 600_000;
            });
        }

        //public static void ConfigureRedisService(this IServiceCollection services)
        //{
        //    services.AddStackExchangeRedisCache(options =>
        //    {
        //        options.Configuration = "localhost:6379";
        //        options.InstanceName = "SaccoShop";
        //    });
        //}
        //public static void ConfigureActiveUserContext(this IServiceCollection services) => services.AddScoped<IActiveUserContext, ActiveUserContext>();
        public static void ConfigureActiveUserProfileService(this IServiceCollection services) => services.AddScoped<IActiveUserProfileService, ActiveUserProfileService>();
        //public static void ConfigureSqlContext(this IServiceCollection services,
        //IConfiguration configuration) =>services.AddDbContext<ApplicationDbContext>(opts =>opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<ApplicationUser, IdentityRole>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = true;
                o.Password.RequireUppercase = true;
                o.Password.RequireNonAlphanumeric = true;
                o.Password.RequiredLength = 5;
                o.User.RequireUniqueEmail = true;
                o.Lockout.MaxFailedAccessAttempts = 3;
                o.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
                o.SignIn.RequireConfirmedPhoneNumber = false;
            }).AddDefaultTokenProviders()

            .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
        }

        public static void ConfigureSlugService(this IServiceCollection services)
        { 
        
        
        services.AddSingleton<SlugService>();
        }

        //public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        //{
        //    var jwtSettings = configuration.GetSection("JwtSettings");

            //    var secretKey = configuration["JwtSettings:SecretKey"];
            //    if (string.IsNullOrWhiteSpace(secretKey))
            //    {

            //        throw new InvalidOperationException("JWT Secret Key is not configured.");
            //    }

            //    var keyBytes = Encoding.UTF8.GetBytes(secretKey);

            //    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //        .AddJwtBearer(options =>
            //        {
            //            options.RequireHttpsMetadata = true;
            //            options.SaveToken = true;
            //            options.TokenValidationParameters = new TokenValidationParameters
            //            {
            //                ValidateIssuer = true,
            //                ValidateAudience = true,
            //                ValidateLifetime = true,
            //                ValidateIssuerSigningKey = true,
            //                ValidIssuer = jwtSettings["validIssuer"],
            //                ValidAudience = jwtSettings["validAudience"],
            //                ClockSkew = TimeSpan.FromSeconds(30),
            //                IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
            //            };
            //        });
            //}



        public static void ConfigureJwt(this IServiceCollection services,
  IConfiguration configuration)
        {
            var jwtSection = configuration.GetSection("JwtSettings");

            var secretKey = jwtSection["SecretKey"];
            if (string.IsNullOrWhiteSpace(secretKey))
                throw new InvalidOperationException("JWT SecretKey is missing.");

            var signingKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(secretKey));
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }) 
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = false;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = jwtSection["ValidIssuer"],
                        ValidAudience = jwtSection["ValidAudience"],
                        IssuerSigningKey = signingKey,

                        ClockSkew = TimeSpan.Zero,
                    };
                });
        }
        public static void ConfigureSqlContext(
    this IServiceCollection services,
    IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

                options.EnableSensitiveDataLogging();
                options.LogTo(Console.WriteLine, LogLevel.Information);
            });
        }
        //public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>services.AddDbContext<ApplicationDbContext>(opts =>opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        //public static void ConfigureSqlContext1(this IServiceCollection services,  IConfiguration configuration) => services.AddSqlServer<ApplicationDbContext>((configuration.GetConnectionString("DefaultConnection")));

    }
}