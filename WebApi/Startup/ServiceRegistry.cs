using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using WebApi.Attributes;
using WebApi.Context;
using WebApi.Domain;
using WebApi.Logging;
using WebApi.Mapper;
using WebApi.Middlewares;
using WebApi.Services;
using Microsoft.EntityFrameworkCore.InMemory;

namespace WebApi.Startup
{
    public static class ServiceRegistry
    {
        public static void Register(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            services.AddHostedService<GlobalTimer>();

            services.AddHttpContextAccessor();

            // Scaffold-DbContext "Data Source=.;Initial Catalog=LogDB;Integrated Security=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Context -Force

            services.AddDbContext<LogDbContext>(option => option.UseSqlServer(configuration["ApplicationOptions:LogDBConnectionString"]));

            //services.AddDbContext<LogDbContext>(opt => opt.UseInMemoryDatabase("LogDB")); // change also in dbcontext

            services.AddScoped<LogDbContext>();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(PermissionAttribute));

            services.AddScoped(typeof(LoggingMiddleware));

            services.AddScoped(typeof(ExceptionHandlingMiddleware));

            //services.AddScoped(typeof(ValidationMiddleware<WeatherForecast>));

            services.AddOptions<CustomOptions>().Bind(configuration.GetSection("ApplicationOptions"));

            services.AddScoped(typeof(LoggingBehaviour<,>));

            // Auto Mapper Config ...

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile(new MapperProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();

            services.AddSingleton(mapper);

            // Swagger 
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(a =>
            {
                a.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                a.AddSecurityRequirement(new OpenApiSecurityRequirement{{
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type =  ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
                    }
                });
            });


        }
    }
}
