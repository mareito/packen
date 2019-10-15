using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using score.Configuration.AutoMapper;
using score.Data;
using score.Infrastructure;
using score.Services.RequestData;
using score.Services.SecurityService;
using score.Services.UserService;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;

namespace score.Configuration
{
    /// <summary>
    /// Extension class for services configuration.  This methods must be called from Startup class.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Configures MySQL context
        /// </summary>
        /// <param name="services">Class that implements IServiceCollection inteface</param>
        /// <param name="Configuration">Class that implements IConfiguration inteface</param>
        /// <returns>Class services</returns>
        public static IServiceCollection AddMySQLContext(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<ApplicationContext>(options => options.UseMySql(Configuration.GetConnectionString("DbConnection")));
            Log.Logger.Information("Application Context added");
            return services;
        }

        /// <summary>
        /// Configures Swagger 
        /// </summary>
        /// <param name="services">Class that implements IServiceCollection inteface</param>
        /// <returns>Class Services</returns>
        public static IServiceCollection AddSwaggerConf(this IServiceCollection services)
        {
            services.AddSwaggerGen(options => 
            {
                options.SwaggerDoc("v1", new Info {
                    Version = "v1",
                    Title = Constants.SwaggerConfiguration.Title,
                    Description = Constants.SwaggerConfiguration.Description,
                    Contact = new Contact {
                        Name = Constants.SwaggerConfiguration.ContactName,
                        Email = Constants.SwaggerConfiguration.ContactEmail,
                        Url = Constants.SwaggerConfiguration.ContactUrl
                    }
                });
                var xmlDocumentationName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlDocumentationPath = Path.Combine(AppContext.BaseDirectory, xmlDocumentationName);
                if (File.Exists(xmlDocumentationPath))
                {
                    options.IncludeXmlComments(xmlDocumentationPath);
                }                
            });
            Log.Logger.Information("Swagger configured");
            return services;
        }

        /// <summary>
        /// Register mediator for CQRS implementation
        /// </summary>
        /// <param name="services">Class that implements IServiceCollection inteface</param>
        /// <returns>Class Services</returns>
        public static IServiceCollection AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup));
            return services;
        }

        /// <summary>
        /// Register custom services for the application
        /// </summary>
        /// <param name="services">Class that implements IServiceCollection inteface</param>
        /// <returns>Class Services</returns>
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddTransient<ISecurityService, SecurityService>();
            services.AddTransient<IUserService, UserService>();
            services.AddScoped<IRequestData, RequestData>();
            services.AddScoped<JwtActionFilter>();

            return services;
        }


        /// <summary>
        /// Register configuration for MAPPER package
        /// </summary>
        /// <param name="services">Class that implements IServiceCollection inteface</param>
        /// <returns>Class Services</returns>
        public static IServiceCollection AddAutomapperConf(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }
    }
}
