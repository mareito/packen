using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Builder.Internal;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace score.Configuration
{
    /// <summary>
    /// Extension methods for application builder
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Swagger endpoint configuration
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder app)
        {
            SwaggerBuilderExtensions.UseSwagger(app);
            app.UseSwagger();                  
            app.UseSwaggerUI(options => {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "API - BOWLING SCORE");
            });
            Log.Logger.Information("Swagger Ready");
            return app;
        }
    }
}
